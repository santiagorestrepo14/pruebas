using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.State;

// PATRÓN STATE
public sealed class ProcessedOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Processed;

    public void EnsureCanQueue()
    {
        throw new DomainException("No se puede enviar a cola un pedido procesado.");
    }

    public void EnsureCanProcess()
    {
        throw new DomainException("El pedido ya fue procesado.");
    }

    public void EnsureCanInvoice()
    {
    }
}
