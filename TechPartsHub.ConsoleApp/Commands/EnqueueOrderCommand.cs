using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class EnqueueOrderCommand : IMenuCommand
{
    public string Key => "10";
    public string Description => "Enviar pedido a cola";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orderId = InputReader.ReadGuid("Id Pedido: ");

        await context.OrderService.EnqueueOrderAsync(orderId);
        Console.WriteLine("Pedido enviado a cola.");
    }
}
