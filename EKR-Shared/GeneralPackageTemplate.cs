namespace EKR_Shared
{
    /// <summary>
    /// Шаблон единого пакета, которым общается приложение.
    /// </summary>
    public class GeneralPackageTemplate
    {
        /// <summary>
        /// Зашифрованный контент пакета.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Указывает на какой обработчик подавать пакет.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Зашифрованный AES ключ для расшифровки.
        /// </summary>
        public byte[] AESKey { get; set; }

        /// <summary>
        /// Вектор инициализации.
        /// </summary>
        public byte[] IV { get; set; }

        /// <summary>
        /// Хеш сумма пакета.
        /// </summary>
        public byte[] Hash { get; set; }
    }
}