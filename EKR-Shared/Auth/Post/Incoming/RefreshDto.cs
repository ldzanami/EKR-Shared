using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Auth.Post.Incoming
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
