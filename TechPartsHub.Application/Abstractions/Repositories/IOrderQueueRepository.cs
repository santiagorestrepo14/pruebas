namespace TechPartsHub.Application.Abstractions.Repositories;

public interface IOrderQueueRepository
{
    Task EnqueueAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Guid?> DequeueAsync(CancellationToken cancellationToken = default);
    Task<bool> ContainsAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Guid>> GetAllAsync(CancellationToken cancellationToken = default);
}
