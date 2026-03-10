using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services.Payments;

// PATRÓN STRATEGY
public sealed class TransferPaymentStrategy : IPaymentStrategy
{
    public string MethodName => "Transferencia";

    public PaymentResult Pay(decimal amount)
    {
        if (amount <= 0) throw new DomainException("El monto de pago debe ser mayor a cero.");

        var tx = $"TRF-{Guid.NewGuid():N}";
        return new PaymentResult(true, tx, "Pago por transferencia confirmado.");
    }
}
