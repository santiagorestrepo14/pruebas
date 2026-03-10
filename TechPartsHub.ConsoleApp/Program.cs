using TechPartsHub.Application.Notifications;
using TechPartsHub.Application.Services;
using TechPartsHub.ConsoleApp.Commands;
using TechPartsHub.ConsoleApp.Factories;
using TechPartsHub.ConsoleApp.Notifications;
using TechPartsHub.ConsoleApp.UI;
using TechPartsHub.Infrastructure.Repositories;
using TechPartsHub.Infrastructure.Seed;

var sparePartRepository = new InMemorySparePartRepository();
var orderRepository = new InMemoryOrderRepository();
var queueRepository = new InMemoryOrderQueueRepository();
var invoiceRepository = new InMemoryInvoiceRepository();
var paymentRepository = new InMemoryPaymentRepository();

await SeedData.PopulateAsync(sparePartRepository);

var pricingService = new OrderPricingService();
var paymentService = new PaymentService(invoiceRepository, paymentRepository);

// PATRÓN OBSERVER: subject + observers concretos registrados en composición raíz.
var stockNotificationCenter = new StockNotificationCenter();
stockNotificationCenter.Subscribe(new ConsoleLowStockObserver(threshold: 5));

var context = new ApplicationContext
{
    InventoryService = new InventoryService(sparePartRepository),
    OrderService = new OrderService(orderRepository, sparePartRepository, queueRepository),
    OrderProcessingService = new OrderProcessingService(orderRepository, sparePartRepository, queueRepository, stockNotificationCenter),
    BillingService = new BillingService(orderRepository, invoiceRepository, pricingService),
    OrderPricingService = pricingService,
    PaymentService = paymentService
};

var commands = CommandFactory.CreateAll();

while (true)
{
    Console.WriteLine("\n=== TechPartsHub ===");
    foreach (var cmd in commands.Values.OrderBy(x => x.Key == "0" ? "99" : x.Key.PadLeft(2, '0')))
        Console.WriteLine($"{cmd.Key}. {cmd.Description}");

    var key = InputReader.ReadRequiredText("Seleccione opción: ");

    if (!commands.TryGetValue(key, out IMenuCommand? command))
    {
        Console.WriteLine("Opción inválida.");
        continue;
    }

    try
    {
        await command.ExecuteAsync(context);
        if (command is ExitCommand)
            break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
