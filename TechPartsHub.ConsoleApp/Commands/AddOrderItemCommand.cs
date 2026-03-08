using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class AddOrderItemCommand : IMenuCommand
{
    public string Key => "7";
    public string Description => "Agregar ítem a pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.Write("Id Pedido: ");
        var orderId = Guid.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("Id Repuesto: ");
        var partId = Guid.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("Cantidad: ");
        var quantity = int.Parse(Console.ReadLine() ?? "0");

        await context.OrderService.AddItemAsync(orderId, partId, quantity);
        Console.WriteLine("Ítem agregado correctamente.");
    }
}
