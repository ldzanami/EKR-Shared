using Confluent.Kafka;
using EKR_Shared.Data;
using EKR_Shared.Exceptions;
using EKR_Shared.Handlers.Interfaces;
using EKR_Shared.Services.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text;
using System.Text.Json;

namespace EKR_Shared.Services.Infrastructure
{
    public class KafkaConsumerService(IConfiguration configuration,
                                      IServiceScopeFactory factory) : BackgroundService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IServiceScopeFactory _factory = factory;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:Address"],
                GroupId = _configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SessionTimeoutMs = int.Parse(_configuration["Kafka:Timeout"]!),
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe(_configuration["Kafka:ConsumerTopicName"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<string, string> result = new();
                try
                {
                    result = consumer.Consume(stoppingToken);
                    var source = Encoding.UTF8.GetString(result.Message.Headers.GetLastBytes("source"));

                    if (source == string.Join("-", _configuration["SelfId"]!.Split('-')[..^1])) continue;

                    using var scope = _factory.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<IKafkaMessageHandler<string, string>>();
                    Log.Information("ПОЛУЧЕНО ВНУТРЕННЕЕ СООБЩЕНИЕ: ID запроса={@Key}, Сообщение={@Value}", result.Message.Key, result.Message.Value);
                    Log.Information("СООБЩЕНИЕ ПЕРЕДАНО ХЕНДЛЕРУ");

                    bool handlerAnswer = await handler.HandleAsync(result.Message, stoppingToken);
                    Log.Information("ХЕНДЛЕР ОБРАБОТАЛ СООБЩЕНИЕ, РЕЗУЛЬТАТ: {@res}", handlerAnswer);

                    consumer.Commit();
                }
                catch (OperationCanceledException ex) when (ex.CancellationToken == stoppingToken)
                {
                    Log.Warning("ОПЕРАЦИЯ ОТМЕНЕНА");
                    throw new ClientSideException(EKRExceptionsText.OperationCancelled, ex);
                }
                catch (Exception ex)
                {
                    using var scope = _factory.CreateScope();
                    var producer = scope.ServiceProvider.GetRequiredService<IKafkaProducerService>();
                    Log.Error(ex.Message);
                    await producer.GiveAnswerAsync(JsonSerializer.Serialize(result), Encoding.UTF8.GetString(result.Message.Headers.GetLastBytes("request-id")), topic: "auth-requests-dlq");
                    throw new ServerSideException(EKRExceptionsText.UnableToProcess, ex);
                }
            }

            consumer.Close();
        }
    }
}
