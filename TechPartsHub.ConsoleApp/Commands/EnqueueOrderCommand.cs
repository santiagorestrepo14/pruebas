using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class EnqueueOrderCommand : IMenuCommand
{
    public string Key => "9";
    public string Description => "Enviar pedido a cola";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.Write("Id Pedido: ");
        var orderId = Guid.Parse(Console.ReadLine() ?? string.Empty);

        await context.OrderService.EnqueueOrderAsync(orderId);
        Console.WriteLine("Pedido enviado a cola.");
    }
}
