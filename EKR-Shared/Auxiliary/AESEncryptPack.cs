namespace EKR_Shared.Auxiliary
{
    public class AESEncryptPack
    {
        public byte[] AESKey { get; set; }

        public byte[] EncryptedAESKey { get; set; }

        public byte[] IV { get; set; }

        public byte[] Content { get; set; }
    }
}
