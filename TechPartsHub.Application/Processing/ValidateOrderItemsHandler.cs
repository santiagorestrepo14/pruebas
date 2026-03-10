using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Processing;

// PATRÓN CHAIN OF RESPONSIBILITY
public sealed class ValidateOrderItemsHandler : OrderProcessingHandlerBase
{
    protected override Task ExecuteAsync(OrderProcessingContext context, CancellationToken cancellationToken)
    {
        if (!context.Order.Items.Any())
            throw new DomainException("El pedido no contiene ítems para procesar.");

        return Task.CompletedTask;
    }
}
