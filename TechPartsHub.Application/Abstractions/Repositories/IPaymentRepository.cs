using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Application.Abstractions.Repositories;

// PATRÓN REPOSITORY
public interface IPaymentRepository
{
    Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Payment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
}
