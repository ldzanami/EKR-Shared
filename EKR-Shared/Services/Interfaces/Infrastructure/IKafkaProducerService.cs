namespace EKR_Shared.Services.Interfaces.Infrastructure
{
    public interface IKafkaProducerService
    {
        /// <summary>
        /// Асинхронно отправляет ответ.
        /// </summary>
        /// <param name="answer">Ответ от сервиса.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="requestId">Id запроса.</param>
        /// <param name="address">Адрес сервера Kafka.</param>
        Task GiveAnswerAsync(string answer, string requestId, string topic = null, string address = null);
    }
}
