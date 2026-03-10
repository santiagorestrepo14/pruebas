using TechPartsHub.Application.Abstractions.Notifications;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.ConsoleApp.Notifications;

// PATRÓN OBSERVER: observador concreto que notifica en consola.
public sealed class ConsoleLowStockObserver : IStockObserver
{
    private readonly int _threshold;

    public ConsoleLowStockObserver(int threshold)
    {
        _threshold = threshold;
    }

    public Task OnLowStockAsync(SparePart sparePart, CancellationToken cancellationToken = default)
    {
        if (sparePart.Stock <= _threshold)
            Console.WriteLine($"[ALERTA STOCK] '{sparePart.Name}' quedó con stock bajo: {sparePart.Stock} (umbral: {_threshold}).");

        return Task.CompletedTask;
    }
}
