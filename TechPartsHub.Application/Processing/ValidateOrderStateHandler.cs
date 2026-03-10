namespace TechPartsHub.Application.Processing;

// PATRÓN CHAIN OF RESPONSIBILITY
public sealed class ValidateOrderStateHandler : OrderProcessingHandlerBase
{
    protected override Task ExecuteAsync(OrderProcessingContext context, CancellationToken cancellationToken)
    {
        context.Order.EnsureCanProcess();
        return Task.CompletedTask;
    }
}
