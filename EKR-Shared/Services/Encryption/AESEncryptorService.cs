using EKR_Shared.Data;
using EKR_Shared.Exceptions;
using EKR_Shared.Services.Interfaces.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace EKR_Shared.Services.Encryption
{
    /// <summary>
    /// Сервис для шифрования AES алгоритмом.
    /// </summary>
    public class AESEncryptorService : IAESEncryptorService
    {
        /// <summary>
        /// Дешифрует данные AES256-CBC алгоритмом.
        /// </summary>
        /// <param name="aesKey">AES256 ключ.</param>
        /// <param name="IV">Вектор инициализации.</param>
        /// <param name="content">Данные для дешифровки.</param>
        /// <returns>Дешифрованные данные.</returns>
        /// <exception cref="ServerSideException"/>
        public string Decrypt(byte[] aesKey, byte[] IV, byte[] content)
        {
            try
            {
                using var aes = Aes.Create();
                aes.KeySize = 256;
                aes.Key = aesKey;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using var decryptor = aes.CreateDecryptor();
                byte[] plain = decryptor.TransformFinalBlock(content, 0, content.Length);
                return Encoding.UTF8.GetString(plain);
            }
            catch (Exception ex)
            {
                throw new ServerSideException(EKRExceptionsText.DecryptAESError, ex);
            }
        }

        /// <summary>
        /// Шифрует данные AES256-CBC алгоритмом.
        /// </summary>
        /// <param name="aesKey">AES256 ключ.</param>
        /// <param name="IV">Вектор инициализации.</param>
        /// <param name="plainText">Данные для шифровки.</param>
        /// <returns>Шифрованные данные.</returns>
        /// <exception cref="ServerSideException"/>
        public byte[] Encrypt(string plainText, byte[] aesKey, byte[] IV)
        {
            try
            {
                using var aes = Aes.Create();
                aes.Key = aesKey;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception ex)
            {
                throw new ServerSideException(EKRExceptionsText.EncryptAESError, ex);
            }
        }
    }
}
