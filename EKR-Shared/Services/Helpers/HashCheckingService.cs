using EKR_Shared.Services.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using EKR_Shared.Exceptions;
using EKR_Shared.Data;

namespace EKR_Shared.Services.Helpers
{
    public class HashCheckingService : IHashCheckingService
    {
        public string CalculateHash<T>(T dto)
        {
            var json = JsonSerializer.Serialize(dto);

            using var doc = JsonDocument.Parse(json);

            var normalizedJson = Normalize(doc.RootElement);

            var bytes = Encoding.UTF8.GetBytes(normalizedJson);

            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash).ToLower();
        }

        private string Normalize(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Object => NormalizeObject(element),
                JsonValueKind.Array => NormalizeArray(element),
                _ => element.ToString()
            };
        }

        private string NormalizeObject(JsonElement obj)
        {
            var props = obj.EnumerateObject()
                           .OrderBy(p => p.Name)
                           .Select(p => $"\"{p.Name}\":\"{Normalize(p.Value)}\"");

            return "{" + string.Join(",", props) + "}";
        }

        private string NormalizeArray(JsonElement array)
        {
            var items = array.EnumerateArray()
                             .Select(Normalize);

            return "[" + string.Join(",", items) + "]";
        }

        public async Task CheckHashAsync<T>(string packageHash, T dto)
        {
            string hash;
            try
            {
                hash = CalculateHash<T>(dto);
            }
            catch (Exception ex)
            {
                throw new ServerSideException(EKRExceptionsText.HashCalculateError, ex);
            }

            if(packageHash != hash)
            {
                throw new ClientSideException("Hash сумма не совпадает");
            }
        }
    }
}