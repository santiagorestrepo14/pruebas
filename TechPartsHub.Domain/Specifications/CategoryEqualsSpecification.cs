using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Domain.Specifications;

// PATRÓN SPECIFICATION
public sealed class CategoryEqualsSpecification : ISpecification<SparePart>
{
    private readonly string _category;

    public CategoryEqualsSpecification(string category)
    {
        _category = category.Trim();
    }

    public bool IsSatisfiedBy(SparePart candidate)
    {
        return string.Equals(candidate.Category, _category, StringComparison.OrdinalIgnoreCase);
    }
}
