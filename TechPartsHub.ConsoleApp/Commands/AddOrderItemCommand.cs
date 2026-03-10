using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class AddOrderItemCommand : IMenuCommand
{
    public string Key => "7";
    public string Description => "Agregar ítem a pedido";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orderId = InputReader.ReadGuid("Id Pedido: ");
        var partId = InputReader.ReadGuid("Id Repuesto: ");
        var quantity = InputReader.ReadInt("Cantidad: ", 1);

        await context.OrderService.AddItemAsync(orderId, partId, quantity);
        Console.WriteLine("Ítem agregado correctamente.");
    }
}
