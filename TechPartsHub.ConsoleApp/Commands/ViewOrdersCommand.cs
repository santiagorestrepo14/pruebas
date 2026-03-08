using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ViewOrdersCommand : IMenuCommand
{
    public string Key => "11";
    public string Description => "Ver pedidos";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orders = await context.OrderService.GetOrdersAsync();
        Console.WriteLine("\n=== Pedidos ===");
        foreach (var order in orders)
        {
            Console.WriteLine($"Pedido {order.Id} | Estado:{order.Status} | Ítems:{order.Items.Count} | Total:{order.GetSubtotal()}");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"  - {item.SparePartName} x{item.Quantity} (${item.UnitPrice})");
            }
        }
    }
}
