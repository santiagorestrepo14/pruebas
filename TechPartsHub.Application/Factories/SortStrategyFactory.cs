using TechPartsHub.Domain.Strategy;

namespace TechPartsHub.Application.Factories;

// PATRÓN SIMPLE FACTORY: centraliza creación de estrategias de ordenamiento.
public static class SortStrategyFactory
{
    public static ISparePartSortStrategy Create(string criterion)
    {
        return criterion.Trim().ToLowerInvariant() switch
        {
            "nombre" => new SortByNameStrategy(),
            "precio" => new SortByPriceStrategy(),
            "stock" => new SortByStockStrategy(),
            _ => throw new ArgumentException("Criterio de orden no soportado. Use: nombre, precio o stock.")
        };
    }
}
