using EKR_Shared.Auxiliary.DeviceInfo;

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
        /// Информация об устройстве.
        /// </summary>
        public DeviceInfoDto DeviceInfo { get; set; }

        /// <summary>
        /// Refresh токен.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
