using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services;

public sealed class OrderPricingService
{
    // SRP: lógica financiera concentrada fuera de Order.
    public decimal CalculateSubtotal(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (!order.Items.Any())
            throw new DomainException("No se puede calcular total para un pedido sin ítems.");

        return order.Items.Sum(item => item.GetLineTotal());
    }
}
