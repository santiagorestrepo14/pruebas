using TechPartsHub.Application.Abstractions.Processing;
using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Application.Notifications;
using TechPartsHub.Application.Processing;
using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Application.Services;

public sealed class OrderProcessingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISparePartRepository _sparePartRepository;
    private readonly IOrderQueueRepository _orderQueueRepository;
    private readonly StockNotificationCenter _stockNotificationCenter;

    public OrderProcessingService(
        IOrderRepository orderRepository,
        ISparePartRepository sparePartRepository,
        IOrderQueueRepository orderQueueRepository,
        StockNotificationCenter stockNotificationCenter)
    {
        _orderRepository = orderRepository;
        _sparePartRepository = sparePartRepository;
        _orderQueueRepository = orderQueueRepository;
        _stockNotificationCenter = stockNotificationCenter;
    }

    public async Task<Guid> ProcessNextAsync(CancellationToken cancellationToken = default)
    {
        var orderId = await _orderQueueRepository.DequeueAsync(cancellationToken)
            ?? throw new DomainException("No hay pedidos en cola para procesar.");

        try
        {
            var order = await _orderRepository.GetByIdAsync(orderId.Value, cancellationToken)
                ?? throw new DomainException("No se puede procesar un pedido inexistente.");

            var parts = await _sparePartRepository.GetAllAsync(cancellationToken);
            var partById = parts.ToDictionary(x => x.Id);

            var context = new OrderProcessingContext
            {
                Order = order,
                SparePartsById = partById
            };

            IOrderProcessingHandler chain = BuildChain();
            await chain.HandleAsync(context, cancellationToken);

            await _orderRepository.UpdateAsync(order, cancellationToken);
            foreach (var part in partById.Values)
            {
                await _sparePartRepository.UpdateAsync(part, cancellationToken);
                await _stockNotificationCenter.NotifyLowStockAsync(part, cancellationToken); // PATRÓN OBSERVER
            }

            return order.Id;
        }
        catch (DomainException)
        {
            throw;
        }
        catch
        {
            if (!await _orderQueueRepository.ContainsAsync(orderId.Value, cancellationToken))
                await _orderQueueRepository.EnqueueAsync(orderId.Value, cancellationToken);
            throw;
        }
    }

    private static IOrderProcessingHandler BuildChain()
    {
        var state = new ValidateOrderStateHandler();
        var items = new ValidateOrderItemsHandler();
        var stock = new ValidateStockHandler();
        var deduct = new DeductStockHandler();
        var mark = new MarkOrderProcessedHandler();

        state.SetNext(items).SetNext(stock).SetNext(deduct).SetNext(mark);
        return state;
    }
}
