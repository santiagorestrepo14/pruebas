using TechPartsHub.Application.Services;

namespace TechPartsHub.ConsoleApp.UI;

public sealed class ApplicationContext
{
    public required InventoryService InventoryService { get; init; }
    public required OrderService OrderService { get; init; }
    public required OrderProcessingService OrderProcessingService { get; init; }
    public required BillingService BillingService { get; init; }
    public required OrderPricingService OrderPricingService { get; init; }
    public required PaymentService PaymentService { get; init; }
}
