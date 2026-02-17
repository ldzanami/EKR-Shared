using Confluent.Kafka;
using EKR_Shared.Services.Interfaces.Infrastructure;
using EKR_Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using EKR_Shared.Data;

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
        /// <param name="partition">Id запроса.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="address">Адрес сервера</param>
        public async Task GiveAnswerAsync(string answer,
                                          string partition = null,
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
                    Key = partition
               };

                var result = await producer.ProduceAsync(topic ?? _configuration["Kafka:ProducerTopicName"], message);

                Log.Information("*ОТПРАВЛЕНО СООБЩЕНИЕ*. ID запроса={@Key}, Сообщение={@Value}", result.Message.Key, result.Message.Value);
            }
            catch (ProduceException<string, string> ex)
            {
                Log.Error(ex, "*ОШИБКА ОТПРАВКИ В KAFKA*");
                throw new ServerSideException(EKRExceptionsText.ProduceError, ex);
            }
        }
    }
}
