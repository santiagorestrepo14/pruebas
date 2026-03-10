using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Domain.Strategy;

// PATRÓN STRATEGY: ordenamiento desacoplado por criterio.
public interface ISparePartSortStrategy
{
    IEnumerable<SparePart> Sort(IEnumerable<SparePart> source);
}
