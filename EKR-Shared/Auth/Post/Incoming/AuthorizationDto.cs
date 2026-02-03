using EKR_Shared.Auxiliary;

namespace EKR_Shared.Auth.Post.Incoming
{
    /// <summary>
    /// Запрос авторизации.
    /// </summary>
    public class AuthorizationDto
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Информация о соединении.
        /// </summary>
        public ConnectionInfoDto ConnectionInfo { get; set; }

        /// <summary>
        /// Refresh токен.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
