using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services.Payments;

// PATRÓN STRATEGY
public sealed class CardPaymentStrategy : IPaymentStrategy
{
    public string MethodName => "Tarjeta";

    public PaymentResult Pay(decimal amount)
    {
        if (amount <= 0) throw new DomainException("El monto de pago debe ser mayor a cero.");

        var tx = $"CARD-{Guid.NewGuid():N}";
        return new PaymentResult(true, tx, "Pago con tarjeta aprobado.");
    }
}
