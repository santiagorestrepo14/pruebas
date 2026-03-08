using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Application.Processing;

public sealed class OrderProcessingContext
{
    public required Order Order { get; init; }
    public required IReadOnlyDictionary<Guid, SparePart> SparePartsById { get; init; }
}
