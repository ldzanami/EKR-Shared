namespace EKR_Shared.Handlers
{
    public interface IKafkaMessageHandler
    {
        Task HandleAsync(string message, CancellationToken ct);
    }
}
