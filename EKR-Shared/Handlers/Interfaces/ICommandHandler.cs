namespace EKR_Shared.Handlers.Interfaces
{
    public interface ICommandHandler
    {
        string CommandType { get; }
        Task<object?> HandleAsync(
            byte[] decryptedContent,
            string requestId,
            CancellationToken ct);
    }
}
