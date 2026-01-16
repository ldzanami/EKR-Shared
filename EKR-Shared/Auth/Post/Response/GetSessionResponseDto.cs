namespace EKR_SharedShared.DTOs.Auth.Post.Response
{
    /// <summary>
    /// Ответ на получение сессии.
    /// </summary>
    public class GetSessionResponseDto
    {
        /// <summary>
        /// Id Сессии.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Информация об устройстве.
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// Дата создания сессии.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата протухания токена.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Отозван ли токен
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Долгоживущий токен.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
