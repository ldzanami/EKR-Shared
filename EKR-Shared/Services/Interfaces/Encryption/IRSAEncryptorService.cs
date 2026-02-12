using EKR_Shared.Services.Encryption;

namespace EKR_Shared.Services.Interfaces.Encryption
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRSAEncryptorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aesKeyEncr"></param>
        /// <param name="IV"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] content);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        byte[] Encrypt(string content);
    }
}
