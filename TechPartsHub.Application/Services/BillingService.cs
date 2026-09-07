using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services;

public sealed class BillingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly OrderPricingService _orderPricingService;

    public BillingService(
        IOrderRepository orderRepository,
        IInvoiceRepository invoiceRepository,
        OrderPricingService orderPricingService)
    {
        _orderRepository = orderRepository;
        _invoiceRepository = invoiceRepository;
        _orderPricingService = orderPricingService;
    }

    public async Task<Invoice> GenerateInvoiceAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var existing = await _invoiceRepository.GetByOrderIdAsync(orderId, cancellationToken);
        if (existing is not null)
            throw new DomainException("El pedido ya tiene una factura generada.");

        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
            ?? throw new DomainException("Pedido inexistente.");

        order.EnsureCanInvoice();
        var total = _orderPricingService.CalculateSubtotal(order);

        var invoice = new Invoice(Guid.NewGuid(), order.Id, DateTime.UtcNow, total);
        await _invoiceRepository.AddAsync(invoice, cancellationToken);
        return invoice;
    }

    public Task<IReadOnlyCollection<Invoice>> GetInvoicesAsync(CancellationToken cancellationToken = default)
        => _invoiceRepository.GetAllAsync(cancellationToken);
}
