using EKR_Shared.Services.Interfaces.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace EKR_Shared.Services.Encryption
{
    /// <summary>
    /// 
    /// </summary>
    public class RSAEncryptorService(IAESEncryptorService AESEncryptorService) : IRSAEncryptorService
    {
        private readonly IAESEncryptorService _AESEncryptorService = AESEncryptorService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aesKeyEncr"></param>
        /// <param name="IV"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] content)
        {
            using var rsa = RSA.Create();
            rsa.ImportFromPem(File.ReadAllText("keys/private.pem"));
            byte[] plain = rsa.Decrypt(content, RSAEncryptionPadding.OaepSHA256);
            return plain;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public byte[] Encrypt(string content)
        {
            using var rsa = RSA.Create();
            rsa.ImportFromPem(File.ReadAllText("keys/public.pem"));
            var data = Encoding.UTF8.GetBytes(content);
            return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
    }
}
