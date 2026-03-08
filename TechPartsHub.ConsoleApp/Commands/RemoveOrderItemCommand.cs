using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class RemoveOrderItemCommand : IMenuCommand
{
    public string Key => "8";
    public string Description => "Remover ítem de pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.Write("Id Pedido: ");
        var orderId = Guid.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("Id Repuesto: ");
        var partId = Guid.Parse(Console.ReadLine() ?? string.Empty);

        await context.OrderService.RemoveItemAsync(orderId, partId);
        Console.WriteLine("Ítem removido correctamente.");
    }
}
