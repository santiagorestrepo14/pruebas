using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Infrastructure.Repositories;

public sealed class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly Dictionary<Guid, Payment> _storage = new();

    public Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        _storage[payment.Id] = payment;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyCollection<Payment>)_storage.Values.OrderByDescending(x => x.PaidAtUtc).ToArray());

    public Task<IReadOnlyCollection<Payment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        var payments = _storage.Values.Where(x => x.InvoiceId == invoiceId).OrderByDescending(x => x.PaidAtUtc).ToArray();
        return Task.FromResult((IReadOnlyCollection<Payment>)payments);
    }
}
