using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Specifications;

namespace TechPartsHub.Application.Factories;

// PATRÓN SIMPLE FACTORY: centraliza creación de especificaciones de búsqueda.
public static class SparePartSpecificationFactory
{
    public static ISpecification<SparePart> Create(string criterion, string value)
    {
        return criterion.Trim().ToLowerInvariant() switch
        {
            "nombre" => new NameContainsSpecification(value),
            "categoria" => new CategoryEqualsSpecification(value),
            "stockmax" => new LowStockSpecification(int.Parse(value)),
            _ => throw new ArgumentException("Criterio de búsqueda no soportado. Use: nombre, categoria o stockmax.")
        };
    }
}
