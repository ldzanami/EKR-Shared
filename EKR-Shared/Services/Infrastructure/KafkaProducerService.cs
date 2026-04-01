using Confluent.Kafka;
using EKR_Shared.Services.Interfaces.Infrastructure;
using EKR_Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Serilog;
using EKR_Shared.Data;
using System.Text.Json;
using System.Text;

namespace EKR_Shared.Services.Infrastructure
{
    /// <summary>
    /// Сервис для отправки ответов в Kafka.
    /// </summary>
    public class KafkaProducerService(IConfiguration configuration) : IKafkaProducerService
    {
        private readonly IConfiguration _configuration = configuration;
        /// <summary>
        /// Асинхронно отправляет ответ.
        /// </summary>
        /// <param name="answer">Ответ от сервиса.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="requestId">Id запроса.</param>
        /// <param name="address">Адрес сервера kafka.</param>
        public async Task GiveAnswerAsync(string answer,
                                          string requestId,
                                          string topic = null,
                                          string address = null)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = address ?? _configuration["Kafka:Address"],
                Acks = Acks.All,
                EnableIdempotence = true,
                RetryBackoffMs = 200
            };

            try
            {
                using var producer = new ProducerBuilder<string, string>(config).Build();

                Message<string, string> message = new()
                {
                    Value = answer,
                    Headers = new()
                    {
                        { "request-id",  Encoding.UTF8.GetBytes(requestId) },
                        { "source", Encoding.UTF8.GetBytes(string.Join("-", _configuration["SelfId"]!.Split('-')[..^1])) }
                    }
                };

                var result = await producer.ProduceAsync(topic ?? _configuration["Kafka:ProducerTopicName"], message);

                Log.Information("ОТПРАВЛЕНО ВНУТРЕННЕЕ СООБЩЕНИЕ. ID запроса={@Key}, Сообщение={@Value}", result.Message.Key, result.Message.Value);
            }
            catch (ProduceException<string, string> ex)
            {
                Log.Error(ex, "ОШИБКА ОТПРАВКИ В KAFKA");
                throw new ServerSideException(EKRExceptionsText.ProduceError, ex);
            }
        }
    }
}
