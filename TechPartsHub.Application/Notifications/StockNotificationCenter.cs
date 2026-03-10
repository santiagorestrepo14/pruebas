using TechPartsHub.Application.Abstractions.Notifications;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Application.Notifications;

// PATRÓN OBSERVER: Subject que administra suscriptores de stock.
public sealed class StockNotificationCenter
{
    private readonly List<IStockObserver> _observers = new();

    public void Subscribe(IStockObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Add(observer);
    }

    public async Task NotifyLowStockAsync(SparePart sparePart, CancellationToken cancellationToken = default)
    {
        foreach (var observer in _observers)
            await observer.OnLowStockAsync(sparePart, cancellationToken);
    }
}
