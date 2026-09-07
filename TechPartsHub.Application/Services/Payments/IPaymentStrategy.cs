namespace TechPartsHub.Application.Services.Payments;

// PATRÓN STRATEGY: contrato de método de pago.
public interface IPaymentStrategy
{
    string MethodName { get; }
    PaymentResult Pay(decimal amount);
}
