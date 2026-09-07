namespace TechPartsHub.Domain.Memento;

// PATRÓN MEMENTO: snapshot de ítems del pedido para restauración segura.
public sealed class OrderItemsMemento
{
    public IReadOnlyCollection<OrderItemSnapshot> Items { get; }

    public OrderItemsMemento(IReadOnlyCollection<OrderItemSnapshot> items)
    {
        Items = items;
    }
}

public sealed class OrderItemSnapshot
{
    public Guid SparePartId { get; }
    public string SparePartName { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }

    public OrderItemSnapshot(Guid sparePartId, string sparePartName, decimal unitPrice, int quantity)
    {
        SparePartId = sparePartId;
        SparePartName = sparePartName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}
