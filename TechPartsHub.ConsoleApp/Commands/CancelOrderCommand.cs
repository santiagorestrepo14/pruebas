using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class CancelOrderCommand : IMenuCommand
{
    public string Key => "13";
    public string Description => "Cancelar pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orderId = InputReader.ReadGuid("Id Pedido: ");
        await context.OrderService.CancelOrderAsync(orderId);
        Console.WriteLine("Pedido cancelado correctamente.");
    }
}
