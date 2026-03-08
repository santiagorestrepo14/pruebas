using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.State;

// PATRÓN STATE
public sealed class CancelledOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Cancelled;

    public void EnsureCanQueue()
    {
        throw new DomainException("No se puede enviar a cola un pedido cancelado.");
    }

    public void EnsureCanProcess()
    {
        throw new DomainException("No se puede procesar un pedido cancelado.");
    }

    public void EnsureCanInvoice()
    {
        throw new DomainException("No se puede facturar un pedido cancelado.");
    }
}
