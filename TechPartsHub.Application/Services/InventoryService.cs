using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Application.Factories;
using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;
using TechPartsHub.Domain.Specifications;

namespace TechPartsHub.Application.Services;

public sealed class InventoryService
{
    private readonly ISparePartRepository _sparePartRepository;

    public InventoryService(ISparePartRepository sparePartRepository)
    {
        _sparePartRepository = sparePartRepository;
    }

    public async Task<SparePart> RegisterSparePartAsync(string sku, string name, string category, decimal unitPrice, int stock, CancellationToken cancellationToken = default)
    {
        var existingBySku = await _sparePartRepository.GetBySkuAsync(sku, cancellationToken);
        if (existingBySku is not null)
            throw new DomainException($"Ya existe un repuesto con SKU '{sku}'.");

        var part = new SparePart(Guid.NewGuid(), sku, name, category, unitPrice, stock);
        await _sparePartRepository.AddAsync(part, cancellationToken);
        return part;
    }

    public Task<IReadOnlyCollection<SparePart>> GetAllAsync(CancellationToken cancellationToken = default)
        => _sparePartRepository.GetAllAsync(cancellationToken);

    public async Task<IReadOnlyCollection<SparePart>> SearchAsync(string criterion, string value, CancellationToken cancellationToken = default)
    {
        ISpecification<SparePart> specification = SparePartSpecificationFactory.Create(criterion, value);
        var parts = await _sparePartRepository.GetAllAsync(cancellationToken);
        return parts.Where(specification.IsSatisfiedBy).ToArray();
    }

    public async Task<IReadOnlyCollection<SparePart>> SortAsync(string criterion, CancellationToken cancellationToken = default)
    {
        var strategy = SortStrategyFactory.Create(criterion);
        var parts = await _sparePartRepository.GetAllAsync(cancellationToken);
        return strategy.Sort(parts).ToArray();
    }

    public async Task<IReadOnlyCollection<SparePart>> GetLowestStockAsync(int k, CancellationToken cancellationToken = default)
    {
        if (k <= 0) throw new DomainException("K debe ser mayor a cero.");

        var parts = await _sparePartRepository.GetAllAsync(cancellationToken);
        return parts.OrderBy(x => x.Stock).Take(k).ToArray();
    }
}
