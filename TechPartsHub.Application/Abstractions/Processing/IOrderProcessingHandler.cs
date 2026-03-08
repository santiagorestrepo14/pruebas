using TechPartsHub.Application.Processing;

namespace TechPartsHub.Application.Abstractions.Processing;

public interface IOrderProcessingHandler
{
    IOrderProcessingHandler SetNext(IOrderProcessingHandler next);
    Task HandleAsync(OrderProcessingContext context, CancellationToken cancellationToken = default);
}
