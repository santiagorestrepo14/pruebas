using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ProcessNextOrderCommand : IMenuCommand
{
    public string Key => "10";
    public string Description => "Procesar siguiente pedido en cola";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var id = await context.OrderProcessingService.ProcessNextAsync();
        Console.WriteLine($"Pedido procesado: {id}");
    }
}
