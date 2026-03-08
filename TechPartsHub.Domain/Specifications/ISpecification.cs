namespace TechPartsHub.Domain.Specifications;

// PATRÓN SPECIFICATION: contrato para encapsular criterios de búsqueda.
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T candidate);
}
