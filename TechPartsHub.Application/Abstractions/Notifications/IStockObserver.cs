using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Application.Abstractions.Notifications;

// PATRÓN OBSERVER: observador para eventos de stock.
public interface IStockObserver
{
    Task OnLowStockAsync(SparePart sparePart, CancellationToken cancellationToken = default);
}
