using Confluent.Kafka;

namespace EKR_Shared.Handlers
{
    public interface IKafkaMessageHandler<TKey, TValue>
    {
        Task HandleAsync(Message<TKey, TValue> message, CancellationToken ct);
    }
}
