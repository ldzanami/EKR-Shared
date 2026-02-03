using System.Text.Json;

namespace EKR_Shared.Auxiliary
{
    /// <summary>
    /// Данные о соединении.
    /// </summary>
    public class ConnectionInfoDto
    {
        /// <summary>
        /// IP сессии.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Имя браузера.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Информация об ОС.
        /// </summary>
        public string OSDescription { get; set; }

        /// <summary>
        /// Неявное преобразование из string json в ConnectionInfoDto.
        /// </summary>
        /// <param name="json">string json ConnectionInfoDto</param>
        public static implicit operator ConnectionInfoDto(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;

            return JsonSerializer.Deserialize<ConnectionInfoDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        /// <summary>
        /// Неявное преобразование из ConnectionInfoDto в string json.
        /// </summary>
        /// <param name="info">ConnectionInfoDto</param>
        public static implicit operator string(ConnectionInfoDto info)
        {
            if (info == null) return null;

            return JsonSerializer.Serialize(info, new JsonSerializerOptions
            {
                WriteIndented = false
            });
        }

        /// <summary>
        /// Сравнивает экземпляр с объектом.
        /// </summary>
        /// <param name="obj">объект для сравнения.</param>
        /// <returns>true если равны; иначе false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not ConnectionInfoDto other) return false;
            return Browser == other.Browser
                   && OSDescription == other.OSDescription
                   && IP == other.IP;
        }

        /// <summary>
        /// Получает хеш код из объекта.
        /// </summary>
        /// <returns>Хеш код.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Browser, OSDescription, IP);
        }

        /// <summary>
        /// Сравнивает 2 экземпляра ConnectionInfoDto
        /// </summary>
        /// <param name="a">Экземпляр ConnectionInfoDto.</param>
        /// <param name="b">Экземпляр ConnectionInfoDto.</param>
        /// <returns>true если равны; иначе false.</returns>
        public static bool operator ==(ConnectionInfoDto a, ConnectionInfoDto b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        /// <summary>
        /// Сравнивает 2 экземпляра ConnectionInfoDto
        /// </summary>
        /// <param name="a">Экземпляр ConnectionInfoDto.</param>
        /// <param name="b">Экземпляр ConnectionInfoDto.</param>
        /// <returns>true если не равны; иначе false.</returns>
        public static bool operator !=(ConnectionInfoDto a, ConnectionInfoDto b) => !(a == b);
    }
}
