using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class CreateOrderCommand : IMenuCommand
{
    public string Key => "6";
    public string Description => "Crear pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var order = await context.OrderService.CreateOrderAsync();
        Console.WriteLine($"Pedido creado: {order.Id}");
    }
}
