using TechPartsHub.Application.Notifications;
using TechPartsHub.Application.Services;
using TechPartsHub.Domain.Entities;
using TechPartsHub.Domain.Enums;
using TechPartsHub.Domain.Exceptions;
using TechPartsHub.Infrastructure.Repositories;

namespace TechPartsHub.Tests;

public sealed class OrderLifecycleTests
{
    [Fact]
    public async Task ProcessNextAsync_WithAvailableStock_ProcessesOrderAndDeductsStock()
    {
        var fixture = await ProcessingFixture.CreateAsync(stock: 5, requestedQuantity: 2);

        var processedOrderId = await fixture.Service.ProcessNextAsync();

        Assert.Equal(fixture.Order.Id, processedOrderId);
        Assert.Equal(OrderStatus.Processed, fixture.Order.Status);
        Assert.Equal(3, fixture.Part.Stock);
        Assert.False(await fixture.Queue.ContainsAsync(fixture.Order.Id));
    }

    [Fact]
    public async Task ProcessNextAsync_WhenBusinessRuleFails_ReturnsOrderToQueueWithoutChangingIt()
    {
        var fixture = await ProcessingFixture.CreateAsync(stock: 1, requestedQuantity: 2);

        await Assert.ThrowsAsync<DomainException>(() => fixture.Service.ProcessNextAsync());

        Assert.True(await fixture.Queue.ContainsAsync(fixture.Order.Id));
        Assert.Equal(new[] { fixture.Order.Id }, await fixture.Queue.GetAllAsync());
        Assert.Equal(OrderStatus.Pending, fixture.Order.Status);
        Assert.Equal(1, fixture.Part.Stock);
    }

    [Fact]
    public void Cancel_WhenOrderIsProcessed_IsRejectedAndKeepsProcessedState()
    {
        var order = CreateOrder(requestedQuantity: 1, Guid.NewGuid());
        order.MarkProcessed();

        Assert.Throws<DomainException>(() => order.Cancel());
        Assert.Equal(OrderStatus.Processed, order.Status);
    }

    private static Order CreateOrder(int requestedQuantity, Guid partId)
    {
        var order = new Order(Guid.NewGuid());
        order.AddItem(new OrderItem(partId, "Test part", 10m, requestedQuantity));
        return order;
    }

    private sealed record ProcessingFixture(
        OrderProcessingService Service,
        InMemoryOrderQueueRepository Queue,
        Order Order,
        SparePart Part)
    {
        public static async Task<ProcessingFixture> CreateAsync(int stock, int requestedQuantity)
        {
            var orders = new InMemoryOrderRepository();
            var parts = new InMemorySparePartRepository();
            var queue = new InMemoryOrderQueueRepository();
            var part = new SparePart(Guid.NewGuid(), "TEST-001", "Test part", "Tests", 10m, stock);
            var order = CreateOrder(requestedQuantity, part.Id);

            await parts.AddAsync(part);
            await orders.AddAsync(order);
            await queue.EnqueueAsync(order.Id);

            var service = new OrderProcessingService(
                orders,
                parts,
                queue,
                new StockNotificationCenter());

            return new ProcessingFixture(service, queue, order, part);
        }
    }
}
