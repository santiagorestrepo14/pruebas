using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Domain.Strategy;

// PATRÓN STRATEGY
public sealed class SortByNameStrategy : ISparePartSortStrategy
{
    public IEnumerable<SparePart> Sort(IEnumerable<SparePart> source)
        => source.OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase);
}
