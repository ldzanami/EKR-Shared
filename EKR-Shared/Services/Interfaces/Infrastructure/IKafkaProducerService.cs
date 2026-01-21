namespace EKR_Shared.Services.Interfaces.Infrastructure
{
    public interface IKafkaProducerService
    {
        /// <summary>
        /// Асинхронно отправляет ответ.
        /// </summary>
        /// <param name="answer">Ответ от сервиса.</param>
        /// <param name="partition">Id запроса.</param>
        /// <param name="topic">Выбранный топик.</param>
        /// <param name="address">Адрес сервера</param>
        Task GiveAnswerAsync(string answer, string partition = null, string topic = null, string address = null);
    }
}
