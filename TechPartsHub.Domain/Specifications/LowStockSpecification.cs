using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Domain.Specifications;

// PATRÓN SPECIFICATION
public sealed class LowStockSpecification : ISpecification<SparePart>
{
    private readonly int _threshold;

    public LowStockSpecification(int threshold)
    {
        _threshold = threshold;
    }

    public bool IsSatisfiedBy(SparePart candidate)
    {
        return candidate.Stock <= _threshold;
    }
}
