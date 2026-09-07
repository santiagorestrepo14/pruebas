using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Infrastructure.Repositories;

public sealed class InMemoryInvoiceRepository : IInvoiceRepository
{
    private readonly Dictionary<Guid, Invoice> _storage = new();

    public Task AddAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        _storage[invoice.Id] = invoice;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyCollection<Invoice>)_storage.Values.OrderByDescending(x => x.IssuedAtUtc).ToArray());

    public Task<Invoice?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var invoice = _storage.Values.FirstOrDefault(x => x.OrderId == orderId);
        return Task.FromResult(invoice);
    }

    public Task<Invoice?> GetByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        _storage.TryGetValue(invoiceId, out var invoice);
        return Task.FromResult(invoice);
    }
}
