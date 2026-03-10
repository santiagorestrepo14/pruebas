using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Application.Abstractions.Repositories;

// PATRÓN REPOSITORY
public interface ISparePartRepository
{
    Task AddAsync(SparePart sparePart, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<SparePart>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SparePart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SparePart?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task UpdateAsync(SparePart sparePart, CancellationToken cancellationToken = default);
}
