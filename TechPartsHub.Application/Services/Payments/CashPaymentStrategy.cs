using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services.Payments;

// PATRÓN STRATEGY
public sealed class CashPaymentStrategy : IPaymentStrategy
{
    public string MethodName => "Efectivo";

    public PaymentResult Pay(decimal amount)
    {
        if (amount <= 0) throw new DomainException("El monto de pago debe ser mayor a cero.");

        var tx = $"CASH-{Guid.NewGuid():N}";
        return new PaymentResult(true, tx, "Pago en efectivo registrado.");
    }
}
