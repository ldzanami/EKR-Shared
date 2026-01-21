using Confluent.Kafka;
using EKR_Shared.Services.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Text.Json;

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
        /// <param name="address">Адрес сервера</param>
        public async Task GiveAnswerAsync(string answer, string topic = null, string address = null)
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

                var result = await producer.ProduceAsync(topic ?? _configuration["Kafka:ProducerTopicName"], new Message<string, string>()
                {
                    Value = answer
                });

                Log.Information("Saved to Kafka. Offset={@Offset}, Content={Message}", result.Offset, result.Message.Value);
            }
            catch (ProduceException<string, string> ex)
            {
                Log.Error(ex, "Kafka produce failed");
            }
        }
        /// <summary>
        /// Асинхронно отправляет ответ в выбранный partition.
        /// </summary>
        /// <param name="answer">Ответ от сервиса.</param>
        /// <param name="partition">Выбранный partition.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="address">Адрес сервера</param>
        public async Task GiveAnswerToPartitionAsync(string answer, GeneralPartitionsEnum partition, string topic = null, string address = null)
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

                var result = await producer.ProduceAsync(topic ?? _configuration["Kafka:ProducerTopicName"], new Message<string, string>()
                {
                    Key = partition.ToString(),
                    Value = answer
                });

                Log.Information("Saved to Kafka. Offset={@Offset}, Content={@Message}", result.Offset, result.Message);
            }
            catch (ProduceException<string, string> ex)
            {
                Log.Error(ex, "Kafka produce failed");
            }
}
    }
}
