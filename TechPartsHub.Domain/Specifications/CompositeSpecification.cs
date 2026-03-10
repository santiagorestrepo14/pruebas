namespace TechPartsHub.Domain.Specifications;

public sealed class CompositeSpecification<T> : ISpecification<T>
{
    private readonly IReadOnlyCollection<ISpecification<T>> _specifications;

    public CompositeSpecification(params ISpecification<T>[] specifications)
    {
        _specifications = specifications;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return _specifications.All(spec => spec.IsSatisfiedBy(candidate));
    }
}
