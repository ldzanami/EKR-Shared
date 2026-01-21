namespace EKR_Shared.Auth.Get.Response
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
    }
}
