namespace TechPartsHub.Application.Processing;

// PATRÓN CHAIN OF RESPONSIBILITY
public sealed class MarkOrderProcessedHandler : OrderProcessingHandlerBase
{
    protected override Task ExecuteAsync(OrderProcessingContext context, CancellationToken cancellationToken)
    {
        context.Order.MarkProcessed();
        return Task.CompletedTask;
    }
}
