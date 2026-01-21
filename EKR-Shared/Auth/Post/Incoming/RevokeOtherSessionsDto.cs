namespace EKR_Shared.Auth.Post.Incoming
{
    /// <summary>
    /// Запрос на выход из всех сессий, кроме текущей.
    /// </summary>
    public class RevokeOtherSessionsDto
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Id текущей сессии.
        /// </summary>
        public Guid KeepSessionId { get; set; }
    }
}
