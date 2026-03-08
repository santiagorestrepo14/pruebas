using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class SortSparePartsCommand : IMenuCommand
{
    public string Key => "4";
    public string Description => "Ordenar repuestos";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.Write("Criterio de orden (nombre/precio/stock): ");
        var criterion = Console.ReadLine() ?? string.Empty;

        var result = await context.InventoryService.SortAsync(criterion);
        Console.WriteLine("\n=== Repuestos ordenados ===");
        foreach (var part in result)
        {
            Console.WriteLine($"{part.Name} | ${part.UnitPrice} | Stock:{part.Stock}");
        }
    }
}
