using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Application.Factories;
using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services;

public sealed class PaymentService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IInvoiceRepository invoiceRepository, IPaymentRepository paymentRepository)
    {
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment> PayInvoiceAsync(Guid invoiceId, string method, CancellationToken cancellationToken = default)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId, cancellationToken)
            ?? throw new DomainException("Factura inexistente.");

        var existingPayments = await _paymentRepository.GetByInvoiceIdAsync(invoiceId, cancellationToken);
        if (existingPayments.Any())
            throw new DomainException("La factura ya fue pagada y no admite pagos duplicados.");

        var strategy = PaymentStrategyFactory.Create(method); // PATRÓN STRATEGY + SIMPLE FACTORY
        var result = strategy.Pay(invoice.Total);

        if (!result.IsSuccessful)
            throw new DomainException($"No fue posible registrar el pago: {result.Message}");

        var payment = new Payment(
            Guid.NewGuid(),
            invoice.Id,
            invoice.Total,
            strategy.MethodName,
            result.TransactionCode,
            DateTime.UtcNow);

        await _paymentRepository.AddAsync(payment, cancellationToken);
        return payment;
    }

    public Task<IReadOnlyCollection<Payment>> GetPaymentsAsync(CancellationToken cancellationToken = default)
        => _paymentRepository.GetAllAsync(cancellationToken);
}
