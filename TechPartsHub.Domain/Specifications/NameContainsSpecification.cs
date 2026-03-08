using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Domain.Specifications;

// PATRÓN SPECIFICATION
public sealed class NameContainsSpecification : ISpecification<SparePart>
{
    private readonly string _text;

    public NameContainsSpecification(string text)
    {
        _text = text.Trim();
    }

    public bool IsSatisfiedBy(SparePart candidate)
    {
        return candidate.Name.Contains(_text, StringComparison.OrdinalIgnoreCase);
    }
}
