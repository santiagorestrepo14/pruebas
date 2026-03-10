using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Domain.Strategy;

// PATRÓN STRATEGY
public sealed class SortByPriceStrategy : ISparePartSortStrategy
{
    public IEnumerable<SparePart> Sort(IEnumerable<SparePart> source)
        => source.OrderBy(x => x.UnitPrice);
}
