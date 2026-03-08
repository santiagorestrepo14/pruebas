using TechPartsHub.Application.Abstractions.Repositories;

namespace TechPartsHub.Infrastructure.Repositories;

public sealed class InMemoryOrderQueueRepository : IOrderQueueRepository
{
    private readonly Queue<Guid> _queue = new();
    private readonly HashSet<Guid> _index = new();

    public Task EnqueueAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        if (_index.Add(orderId))
        {
            _queue.Enqueue(orderId);
        }
        return Task.CompletedTask;
    }

    public Task<Guid?> DequeueAsync(CancellationToken cancellationToken = default)
    {
        if (_queue.Count == 0)
            return Task.FromResult<Guid?>(null);

        var id = _queue.Dequeue();
        _index.Remove(id);
        return Task.FromResult<Guid?>(id);
    }

    public Task<bool> ContainsAsync(Guid orderId, CancellationToken cancellationToken = default)
        => Task.FromResult(_index.Contains(orderId));

    public Task<IReadOnlyCollection<Guid>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyCollection<Guid>)_queue.ToArray());
}
