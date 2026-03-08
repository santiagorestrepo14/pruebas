using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services;

public sealed class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISparePartRepository _sparePartRepository;
    private readonly IOrderQueueRepository _orderQueueRepository;

    public OrderService(IOrderRepository orderRepository, ISparePartRepository sparePartRepository, IOrderQueueRepository orderQueueRepository)
    {
        _orderRepository = orderRepository;
        _sparePartRepository = sparePartRepository;
        _orderQueueRepository = orderQueueRepository;
    }

    public async Task<Order> CreateOrderAsync(CancellationToken cancellationToken = default)
    {
        var order = new Order(Guid.NewGuid());
        await _orderRepository.AddAsync(order, cancellationToken);
        return order;
    }

    public Task<IReadOnlyCollection<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
        => _orderRepository.GetAllAsync(cancellationToken);

    public async Task AddItemAsync(Guid orderId, Guid sparePartId, int quantity, CancellationToken cancellationToken = default)
    {
        if (quantity <= 0) throw new DomainException("La cantidad debe ser mayor a cero.");

        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
            ?? throw new DomainException("Pedido inexistente.");

        var part = await _sparePartRepository.GetByIdAsync(sparePartId, cancellationToken)
            ?? throw new DomainException("Repuesto inexistente.");

        var alreadyRequested = order.GetReservedQuantity(sparePartId);
        if (alreadyRequested + quantity > part.Stock)
            throw new DomainException($"No se puede agregar cantidad mayor al stock disponible considerando el pedido. Disponible: {part.Stock}, en pedido: {alreadyRequested}, a agregar: {quantity}.");

        order.AddItem(new OrderItem(part.Id, part.Name, part.UnitPrice, quantity));
        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task RemoveItemAsync(Guid orderId, Guid sparePartId, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
            ?? throw new DomainException("Pedido inexistente.");

        order.RemoveItem(sparePartId);
        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task EnqueueOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken)
            ?? throw new DomainException("Pedido inexistente.");

        order.EnsureCanQueue();

        if (await _orderQueueRepository.ContainsAsync(orderId, cancellationToken))
            throw new DomainException("No debe permitirse duplicar un pedido dentro de la cola.");

        await _orderQueueRepository.EnqueueAsync(orderId, cancellationToken);
    }
}
