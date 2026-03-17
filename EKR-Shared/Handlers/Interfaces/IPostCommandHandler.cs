using EKR_Shared.Auxiliary;

namespace EKR_Shared.Handlers.Interfaces
{
    public interface IPostCommandHandler
    {
        string CommandType { get; }

        Task<object?> HandleAsync(
            string decryptedContent,
            string requestId,
            AESEncryptPack AESPack,
            CancellationToken ct);
    }
}