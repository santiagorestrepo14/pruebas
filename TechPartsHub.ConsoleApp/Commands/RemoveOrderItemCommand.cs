using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class RemoveOrderItemCommand : IMenuCommand
{
    public string Key => "8";
    public string Description => "Remover ítem de pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orderId = InputReader.ReadGuid("Id Pedido: ");
        var partId = InputReader.ReadGuid("Id Repuesto: ");

        await context.OrderService.RemoveItemAsync(orderId, partId);
        Console.WriteLine("Ítem removido correctamente.");
    }
}
