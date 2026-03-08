using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.Entities;

public sealed class Invoice
{
    public Guid Id { get; }
    public Guid OrderId { get; }
    public DateTime IssuedAtUtc { get; }
    public decimal Total { get; }

    public Invoice(Guid id, Guid orderId, DateTime issuedAtUtc, decimal total)
    {
        if (id == Guid.Empty) throw new DomainException("El Id de la factura es inválido.");
        if (orderId == Guid.Empty) throw new DomainException("El Id del pedido facturado es inválido.");
        if (total <= 0) throw new DomainException("El total de la factura debe ser mayor a cero.");

        Id = id;
        OrderId = orderId;
        IssuedAtUtc = issuedAtUtc;
        Total = total;
    }
}
