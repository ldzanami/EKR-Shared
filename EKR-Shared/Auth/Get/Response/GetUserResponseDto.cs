using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Shared.DTOs.Communication.Users.Get.Response
{
    /// <summary>
    /// Ответ на получение пользователя.
    /// </summary>
    public class GetUserResponseDto
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Путь к аватару пользователя на сервере.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Публичный RSA ключ пользователя.
        /// </summary>
        public required string PublicKey { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Онлайн ли пользователь.
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
