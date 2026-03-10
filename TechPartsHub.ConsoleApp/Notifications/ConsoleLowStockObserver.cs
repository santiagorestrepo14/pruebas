using TechPartsHub.Application.Abstractions.Notifications;
using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.ConsoleApp.Notifications;

// PATRÓN OBSERVER: observador concreto que notifica en consola.
public sealed class ConsoleLowStockObserver : IStockObserver
{
    private readonly int _threshold;

    public ConsoleLowStockObserver(int threshold)
    {
        if (threshold < 0) throw new DomainException("El umbral de stock bajo no puede ser negativo.");
        _threshold = threshold;
    }

    public Task OnLowStockAsync(SparePart sparePart, CancellationToken cancellationToken = default)
    {
        if (sparePart.Stock <= _threshold)
            Console.WriteLine($"[ALERTA STOCK] '{sparePart.Name}' quedó con stock bajo: {sparePart.Stock} (umbral: {_threshold}).");

        return Task.CompletedTask;
    }
}
