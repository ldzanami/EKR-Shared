namespace EKR_Shared.Handlers.Interfaces
{
    public interface IGetCommandHandler
    {
        string CommandType { get; }
        Task<object?> HandleAsync(
            string requestId,
            CancellationToken ct);
    }
}
