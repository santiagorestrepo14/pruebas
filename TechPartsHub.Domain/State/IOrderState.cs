using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.State;

// PATRÓN STATE: interfaz de estado para encapsular reglas por estado del pedido.
public interface IOrderState
{
    OrderStatus Status { get; }
    void EnsureCanQueue();
    void EnsureCanProcess();
    void EnsureCanInvoice();
}
