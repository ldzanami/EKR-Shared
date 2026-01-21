namespace EKR_Shared.Services.Interfaces.Encryption
{
    public interface IAESEncryptorService
    {
        string Decrypt(byte[] aesKey, byte[] IV, byte[] content);
        byte[] Encrypt(string plainText, byte[] aesKey, byte[] IV);
    }
}
