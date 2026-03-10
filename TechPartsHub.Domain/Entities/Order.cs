using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;
using TechPartsHub.Domain.Memento;
using TechPartsHub.Domain.State;

namespace TechPartsHub.Domain.Entities;

public sealed class Order
{
    private readonly List<OrderItem> _items = new();
    private IOrderState _state;

    public Guid Id { get; }
    public DateTime CreatedAtUtc { get; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public OrderStatus Status => _state.Status;

    public Order(Guid id)
    {
        if (id == Guid.Empty) throw new DomainException("El Id del pedido es inválido.");
        Id = id;
        CreatedAtUtc = DateTime.UtcNow;
        _state = new PendingOrderState();
    }

    public void AddItem(OrderItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _state.EnsureCanQueue();

        var existing = _items.FirstOrDefault(x => x.SparePartId == item.SparePartId);
        if (existing is null)
        {
            _items.Add(item);
            return;
        }

        existing.IncreaseQuantity(item.Quantity);
    }

    public void RemoveItem(Guid sparePartId)
    {
        var item = _items.FirstOrDefault(x => x.SparePartId == sparePartId)
            ?? throw new DomainException("El ítem no existe dentro del pedido.");

        _state.EnsureCanQueue();
        _items.Remove(item);
    }

    // PATRÓN MEMENTO: crea snapshot del estado de ítems antes de cambios.
    public OrderItemsMemento CreateItemsMemento()
    {
        var snapshot = _items
            .Select(x => new OrderItemSnapshot(x.SparePartId, x.SparePartName, x.UnitPrice, x.Quantity))
            .ToArray();

        return new OrderItemsMemento(snapshot);
    }

    // PATRÓN MEMENTO: restaura el estado de ítems desde snapshot.
    public void RestoreItemsMemento(OrderItemsMemento memento)
    {
        ArgumentNullException.ThrowIfNull(memento);
        _state.EnsureCanQueue();

        _items.Clear();
        foreach (var snapshot in memento.Items)
            _items.Add(new OrderItem(snapshot.SparePartId, snapshot.SparePartName, snapshot.UnitPrice, snapshot.Quantity));
    }

    public void EnsureCanQueue()
    {
        _state.EnsureCanQueue();
        if (_items.Count == 0)
            throw new DomainException("No se puede enviar a cola un pedido vacío.");
    }

    public void EnsureCanProcess() => _state.EnsureCanProcess();
    public void EnsureCanInvoice() => _state.EnsureCanInvoice();

    public void MarkProcessed()
    {
        EnsureCanProcess();
        _state = new ProcessedOrderState(); // PATRÓN STATE
    }

    public void Cancel()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Solo se puede cancelar un pedido en estado Pendiente.");

        _state = new CancelledOrderState(); // PATRÓN STATE
    }

    public int GetReservedQuantity(Guid sparePartId)
        => _items.Where(x => x.SparePartId == sparePartId).Sum(x => x.Quantity);
}
