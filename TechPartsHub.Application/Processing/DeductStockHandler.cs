namespace TechPartsHub.Application.Processing;

// PATRÓN CHAIN OF RESPONSIBILITY
public sealed class DeductStockHandler : OrderProcessingHandlerBase
{
    protected override Task ExecuteAsync(OrderProcessingContext context, CancellationToken cancellationToken)
    {
        foreach (var item in context.Order.Items)
        {
            var sparePart = context.SparePartsById[item.SparePartId];
            sparePart.DecreaseStock(item.Quantity);
        }

        return Task.CompletedTask;
    }
}
