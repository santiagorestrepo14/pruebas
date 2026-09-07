using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class UndoOrderItemsChangeCommand : IMenuCommand
{
    public string Key => "9";
    public string Description => "Deshacer último cambio de ítems en pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orderId = InputReader.ReadGuid("Id Pedido: ");
        await context.OrderService.UndoLastItemsChangeAsync(orderId);
        Console.WriteLine("Se deshizo el último cambio de ítems del pedido.");
    }
}
