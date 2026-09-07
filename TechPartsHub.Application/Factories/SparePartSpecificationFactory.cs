using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;
using TechPartsHub.Domain.Specifications;

namespace TechPartsHub.Application.Factories;

// PATRÓN SIMPLE FACTORY: centraliza creación de especificaciones de búsqueda.
public static class SparePartSpecificationFactory
{
    public static ISpecification<SparePart> Create(string criterion, string value)
    {
        var normalized = criterion.Trim().ToLowerInvariant();

        return normalized switch
        {
            "nombre" => new NameContainsSpecification(value),
            "categoria" => new CategoryEqualsSpecification(value),
            "stockmax" => CreateLowStockSpecification(value),
            "compuesto" => CreateComposite(value),
            _ => throw new DomainException("Criterio de búsqueda no soportado. Use: nombre, categoria, stockmax o compuesto.")
        };
    }

    private static ISpecification<SparePart> CreateLowStockSpecification(string value)
    {
        if (!int.TryParse(value, out int threshold))
            throw new DomainException("El valor para stockmax debe ser un entero válido.");

        return new LowStockSpecification(threshold);
    }

    private static ISpecification<SparePart> CreateComposite(string value)
    {
        var chunks = value.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (chunks.Length == 0)
            throw new DomainException("Para búsqueda compuesta use formato: nombre:ryzen;categoria:CPU;stockmax:10");

        var specs = new List<ISpecification<SparePart>>();
        foreach (var chunk in chunks)
        {
            var pair = chunk.Split(':', 2, StringSplitOptions.TrimEntries);
            if (pair.Length != 2)
                throw new DomainException($"Segmento inválido '{chunk}'. Formato esperado criterio:valor.");

            specs.Add(Create(pair[0], pair[1]));
        }

        return new CompositeSpecification<SparePart>(specs.ToArray()); // PATRÓN SPECIFICATION (composición)
    }
}
