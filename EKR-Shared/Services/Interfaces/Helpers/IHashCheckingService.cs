using System.Text.Json;

namespace EKR_Shared.Services.Interfaces.Helpers
{
    /// <summary>
    /// Сервис, проверяющий соответствие хеш суммы.
    /// </summary>
    public interface IHashCheckingService
    {
        /// <summary>
        /// Вычисляет хеш сумму пакета.
        /// </summary>
        /// <param name="dto">Пакет GeneralPackageTemplate без хеша в нём.</param>
        /// <returns>Хеш сумма пакета.</returns>
        static abstract string CalculateHash<T>(T dto);

        /// <summary>
        /// Асинхронно проверяет соответствие хеш сумм.
        /// </summary>
        /// <param name="packageHash">Хеш сумма пакета с клиента.</param>
        /// <exception cref="BadHttpRequestException"></exception>
        static abstract Task CheckHashAsync<T>(string packageHash, T dto);
    }
}
