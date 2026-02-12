namespace EKR_Shared
{
    /// <summary>
    /// Шаблон единого пакета, которым общается приложение.
    /// </summary>
    public class GeneralPackageTemplate
    {
        /// <summary>
        /// Id запроса.
        /// </summary>
        /// <remarks>
        /// Автозаполняется.
        /// </remarks>
        public Guid RequestId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Зашифрованный контент пакета.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Указывает на какой обработчик подавать пакет.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Зашифрованный AES ключ для расшифровки.
        /// </summary>
        public string AESKey { get; set; }

        /// <summary>
        /// Вектор инициализации.
        /// </summary>
        public string IV { get; set; }

        /// <summary>
        /// Хеш сумма пакета.
        /// </summary>
        public string Hash { get; set; }
    }
}