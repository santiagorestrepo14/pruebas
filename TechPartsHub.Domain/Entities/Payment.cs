using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.Entities;

public sealed class Payment
{
    public Guid Id { get; }
    public Guid InvoiceId { get; }
    public decimal Amount { get; }
    public string Method { get; }
    public string TransactionCode { get; }
    public DateTime PaidAtUtc { get; }

    public Payment(Guid id, Guid invoiceId, decimal amount, string method, string transactionCode, DateTime paidAtUtc)
    {
        if (id == Guid.Empty) throw new DomainException("El Id del pago es inválido.");
        if (invoiceId == Guid.Empty) throw new DomainException("El Id de factura del pago es inválido.");
        if (amount <= 0) throw new DomainException("El monto de pago debe ser mayor a cero.");
        if (string.IsNullOrWhiteSpace(method)) throw new DomainException("El método de pago es obligatorio.");
        if (string.IsNullOrWhiteSpace(transactionCode)) throw new DomainException("El código de transacción es obligatorio.");

        Id = id;
        InvoiceId = invoiceId;
        Amount = amount;
        Method = method.Trim();
        TransactionCode = transactionCode.Trim();
        PaidAtUtc = paidAtUtc;
    }
}
