using System.ComponentModel.DataAnnotations;

namespace EKR_Shared.Auth.Post.Incoming
{
    /// <summary>
    /// Регистрационный запрос.
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        /// <remarks>Ограничение по длине: 8;
        /// <br>Должнен содержать заглавные, строчные и спецсимволы.</br></remarks>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
