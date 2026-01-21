namespace EKR_Shared.Services.Interfaces.Infrastructure
{
    public interface IKafkaProducerService
    {
        /// <summary>
        /// Асинхронно отправляет ответ.
        /// </summary>
        /// <param name="answer">Ответ от сервиса.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="address">Адрес сервера</param>
        Task GiveAnswerAsync(string answer, string topic = null, string address = null);
        /// <summary>
        /// Асинхронно отправляет ответ в выбранный partition.
        /// </summary>
        /// <param name="answer">Ответ от сервиса.</param>
        /// <param name="partition">Выбранный partition.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="address">Адрес сервера</param>
        Task GiveAnswerToPartitionAsync(string answer, GeneralPartitionsEnum partition, string topic = null, string address = null);
    }
}
