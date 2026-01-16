using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Auth.Post.Response
{
    /// <summary>
    /// Ответ на регистрацию.
    /// </summary>
    public class UserCreatedResponseDto
    {

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Публичный RSA ключ пользователя.
        /// </summary>
        public string PublicKey { get; set; }
    }
}
