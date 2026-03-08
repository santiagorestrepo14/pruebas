using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Infrastructure.Repositories;

public sealed class InMemorySparePartRepository : ISparePartRepository
{
    private readonly Dictionary<Guid, SparePart> _storage = new();

    public Task AddAsync(SparePart sparePart, CancellationToken cancellationToken = default)
    {
        _storage[sparePart.Id] = sparePart;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<SparePart>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyCollection<SparePart>)_storage.Values.ToArray());

    public Task<SparePart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _storage.TryGetValue(id, out var part);
        return Task.FromResult(part);
    }

    public Task<SparePart?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        var part = _storage.Values.FirstOrDefault(x => string.Equals(x.Sku, sku, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(part);
    }

    public Task UpdateAsync(SparePart sparePart, CancellationToken cancellationToken = default)
    {
        _storage[sparePart.Id] = sparePart;
        return Task.CompletedTask;
    }
}
