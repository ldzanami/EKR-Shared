using EKR_Shared.Services.Interfaces.Encryption;
using System.Security.Cryptography;
using System.Text;
using EKR_Shared.Exceptions;
using EKR_Shared.Data;
using Serilog;

namespace EKR_Shared.Services.Encryption
{
    /// <summary>
    /// Сервис для шифрования RSA алгоритмом.
    /// </summary>
    public class RSAEncryptorService : IRSAEncryptorService
    {
        /// <summary>
        /// Метод дешифровки данных алгоритмом RSA.
        /// </summary>
        /// <param name="content">Данные для дешифровки.</param>
        /// <param name="keyVersion">Версия ключа RSA.</param>
        /// <returns>Дешифрованные данные.</returns>
        /// <exception cref="ServerSideException"/>
        public byte[] Decrypt(byte[] content, string keyVersion)
        {
            try
            {
                using var rsa = RSA.Create();
                rsa.ImportFromPem(File.ReadAllText("keys/" + keyVersion + "/private.pem"));
                byte[] plain = rsa.Decrypt(content, RSAEncryptionPadding.OaepSHA256);
                return plain;
            }
            catch (Exception ex)
            {
                throw new ServerSideException(EKRExceptionsText.DecryptRSAError, ex);
            }
        }

        /// <summary>
        /// Метод шифровки данных алгоритмом RSA.
        /// </summary>
        /// <param name="content">Данные для шифровки.</param>
        /// <returns>Шифрованные данные.</returns>
        /// <exception cref="ServerSideException"/>
        public byte[] Encrypt(string content)
        {
            try
            {
                using var rsa = RSA.Create();
                rsa.ImportFromPem(File.ReadAllText("keys/current/public.pem"));
                var data = Encoding.UTF8.GetBytes(content);
                return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
            catch (Exception ex)
            {
                throw new ServerSideException(EKRExceptionsText.EncryptRSAError, ex);
            }
        }
    }
}
