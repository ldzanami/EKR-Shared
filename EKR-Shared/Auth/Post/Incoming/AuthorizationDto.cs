using SecureMessageManager.Shared.DTOs.Auxiliary.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Auth.Post.Incoming
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
