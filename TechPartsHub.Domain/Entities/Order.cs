using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;
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
        _state.EnsureCanQueue(); // Pendiente

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
        _state = new CancelledOrderState(); // PATRÓN STATE
    }

    public int GetReservedQuantity(Guid sparePartId)
    {
        return _items.Where(x => x.SparePartId == sparePartId).Sum(x => x.Quantity);
    }

    public decimal GetSubtotal() => _items.Sum(x => x.GetLineTotal());
}
