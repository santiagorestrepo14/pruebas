using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.Entities;

public sealed class OrderItem
{
    public Guid SparePartId { get; }
    public string SparePartName { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; private set; }

    public OrderItem(Guid sparePartId, string sparePartName, decimal unitPrice, int quantity)
    {
        if (sparePartId == Guid.Empty) throw new DomainException("El Id del repuesto del ítem es inválido.");
        if (string.IsNullOrWhiteSpace(sparePartName)) throw new DomainException("El nombre del ítem es obligatorio.");
        if (unitPrice <= 0) throw new DomainException("El precio del ítem debe ser mayor a cero.");
        if (quantity <= 0) throw new DomainException("La cantidad del ítem debe ser mayor a cero.");

        SparePartId = sparePartId;
        SparePartName = sparePartName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0) throw new DomainException("La cantidad a agregar debe ser mayor a cero.");
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity <= 0) throw new DomainException("La cantidad del ítem debe ser mayor a cero.");
        Quantity = quantity;
    }

    public decimal GetLineTotal() => UnitPrice * Quantity;
}
