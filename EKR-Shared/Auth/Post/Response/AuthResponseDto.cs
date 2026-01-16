using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Auth.Post.Response
{
    /// <summary>
    /// Ответ на авторизацию.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// Id сессии.
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// Краткосрочный токен.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Долгоживущий токен.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username { get; set; }
    }
}
