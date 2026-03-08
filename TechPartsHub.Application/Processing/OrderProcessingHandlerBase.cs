using TechPartsHub.Application.Abstractions.Processing;

namespace TechPartsHub.Application.Processing;

// PATRÓN CHAIN OF RESPONSIBILITY: eslabón base del pipeline de procesamiento.
public abstract class OrderProcessingHandlerBase : IOrderProcessingHandler
{
    private IOrderProcessingHandler? _next;

    public IOrderProcessingHandler SetNext(IOrderProcessingHandler next)
    {
        _next = next;
        return next;
    }

    public async Task HandleAsync(OrderProcessingContext context, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(context, cancellationToken);
        if (_next is not null)
            await _next.HandleAsync(context, cancellationToken);
    }

    protected abstract Task ExecuteAsync(OrderProcessingContext context, CancellationToken cancellationToken);
}
