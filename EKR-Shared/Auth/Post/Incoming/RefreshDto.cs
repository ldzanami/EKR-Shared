namespace EKR_Shared.Auth.Post.Incoming
{
    /// <summary>
    /// Ответ на обновление токенов.
    /// </summary>
    public class RefreshDto
    {
        /// <summary>
        /// Краткосрочный токен.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Долгоживущий токен.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Id сессии.
        /// </summary>
        public Guid SessionId { get; set; }
    }
}
