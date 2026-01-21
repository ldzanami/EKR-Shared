namespace EKR_Shared.Auth.Post.Response
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
    }
}
