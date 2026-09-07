using TechPartsHub.Application.Services.Payments;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Factories;

// PATRÓN SIMPLE FACTORY: crea estrategias de pago sin mezclar reglas de negocio.
public static class PaymentStrategyFactory
{
    public static IPaymentStrategy Create(string method)
    {
        return method.Trim().ToLowerInvariant() switch
        {
            "tarjeta" => new CardPaymentStrategy(),
            "efectivo" => new CashPaymentStrategy(),
            "transferencia" => new TransferPaymentStrategy(),
            _ => throw new DomainException("Método de pago no soportado. Use: tarjeta, efectivo o transferencia.")
        };
    }
}
