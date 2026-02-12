using EKR_Shared.Services.Interfaces.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace EKR_Shared.Services.Encryption
{
    public class AESEncryptorService : IAESEncryptorService
    {
        public string Decrypt(byte[] aesKey, byte[] IV, byte[] content)
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Key = aesKey;
            aes.IV = IV;
            using var decryptor = aes.CreateDecryptor();
            byte[] plain = decryptor.TransformFinalBlock(content, 0, content.Length);
            return Encoding.UTF8.GetString(plain);
        }

        public byte[] Encrypt(string plainText, byte[] aesKey, byte[] IV)
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
    }
}
