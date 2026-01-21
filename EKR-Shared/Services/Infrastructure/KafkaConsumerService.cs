using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using EKR_Shared.Handlers;
using EKR_Shared.Services.Interfaces.Infrastructure;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

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
                    using var scope = _factory.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<IKafkaMessageHandler>();
                    var message = result.Message.Value;
                    Log.Information("Received: {Message}", message);
                    consumer.Commit();
                    await handler.HandleAsync(message, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    Log.Warning("Operation Canceled");
                    break;
                }
                catch (Exception ex)
                {
                    using var scope = _factory.CreateScope();
                    var producer = scope.ServiceProvider.GetRequiredService<IKafkaProducerService>();
                    Log.Error(ex.Message);
                    await producer.GiveAnswerAsync(JsonSerializer.Serialize(result), topic: "auth-requests-dlq");
                }
            }

            consumer.Close();
        }
    }
}
