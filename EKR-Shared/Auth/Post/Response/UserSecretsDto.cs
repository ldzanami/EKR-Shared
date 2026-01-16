using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Auth.Post.Response
{
    /// <summary>
    /// Пользователь с секретами.
    /// </summary>
    public class UserSecretsDto
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя в верхнем регистре.
        /// </summary>
        public string UsernameNormalized { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Хеш пароля пользователя.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Закрытый RSA ключ пользователя.
        /// </summary>
        public byte[] PrivateKeyEnc { get; set; }
    }
}
