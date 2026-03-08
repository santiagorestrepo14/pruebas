using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Processing;

// PATRÓN CHAIN OF RESPONSIBILITY
public sealed class ValidateStockHandler : OrderProcessingHandlerBase
{
    protected override Task ExecuteAsync(OrderProcessingContext context, CancellationToken cancellationToken)
    {
        foreach (var item in context.Order.Items)
        {
            if (!context.SparePartsById.TryGetValue(item.SparePartId, out var part))
                throw new DomainException($"Inconsistencia: el repuesto {item.SparePartName} no existe en inventario.");

            if (item.Quantity > part.Stock)
                throw new DomainException($"Stock insuficiente al procesar: {part.Name}. Solicitado: {item.Quantity}, disponible: {part.Stock}.");
        }

        return Task.CompletedTask;
    }
}
