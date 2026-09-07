using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ViewLowStockCommand : IMenuCommand
{
    public string Key => "5";
    public string Description => "Ver K repuestos con menor stock";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var k = InputReader.ReadInt("K: ", 1);
        var result = await context.InventoryService.GetLowestStockAsync(k);

        Console.WriteLine("\n=== Menor stock ===");
        foreach (var part in result)
            Console.WriteLine($"{part.Name} | Stock:{part.Stock}");
    }
}
