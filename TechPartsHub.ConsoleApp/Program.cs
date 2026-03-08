using TechPartsHub.Application.Services;
using TechPartsHub.ConsoleApp.Commands;
using TechPartsHub.ConsoleApp.Factories;
using TechPartsHub.ConsoleApp.UI;
using TechPartsHub.Infrastructure.Repositories;
using TechPartsHub.Infrastructure.Seed;

var sparePartRepository = new InMemorySparePartRepository();
var orderRepository = new InMemoryOrderRepository();
var queueRepository = new InMemoryOrderQueueRepository();
var invoiceRepository = new InMemoryInvoiceRepository();

await SeedData.PopulateAsync(sparePartRepository);

var context = new ApplicationContext
{
    InventoryService = new InventoryService(sparePartRepository),
    OrderService = new OrderService(orderRepository, sparePartRepository, queueRepository),
    OrderProcessingService = new OrderProcessingService(orderRepository, sparePartRepository, queueRepository),
    BillingService = new BillingService(orderRepository, invoiceRepository)
};

var commands = CommandFactory.CreateAll();

while (true)
{
    Console.WriteLine("\n=== TechPartsHub ===");
    foreach (var cmd in commands.Values.OrderBy(x => x.Key == "0" ? "99" : x.Key.PadLeft(2, '0')))
    {
        Console.WriteLine($"{cmd.Key}. {cmd.Description}");
    }

    Console.Write("Seleccione opción: ");
    var key = Console.ReadLine() ?? string.Empty;

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
