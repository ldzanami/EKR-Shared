namespace EKR_Shared.Services.Encryption
{
    public class RSADecryptedDto
    {
        public byte[] AesKey { get; set; }

        public string Content { get; set; }
    }
}
