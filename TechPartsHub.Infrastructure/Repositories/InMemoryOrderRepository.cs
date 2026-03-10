using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Infrastructure.Repositories;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _storage = new();

    public Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        _storage[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _storage.TryGetValue(id, out var order);
        return Task.FromResult(order);
    }

    public Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyCollection<Order>)_storage.Values.OrderBy(x => x.CreatedAtUtc).ToArray());

    public Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _storage[order.Id] = order;
        return Task.CompletedTask;
    }
}
