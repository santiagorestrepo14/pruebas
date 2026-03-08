using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.State;

// PATRÓN STATE
public sealed class PendingOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Pending;

    public void EnsureCanQueue()
    {
    }

    public void EnsureCanProcess()
    {
    }

    public void EnsureCanInvoice()
    {
        throw new DomainException("No se puede facturar un pedido pendiente. Debe estar procesado.");
    }
}
