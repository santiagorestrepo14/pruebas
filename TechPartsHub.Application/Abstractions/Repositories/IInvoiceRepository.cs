using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Application.Abstractions.Repositories;

// PATRÓN REPOSITORY
public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Invoice?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Invoice?> GetByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
}
