using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class SearchSparePartsCommand : IMenuCommand
{
    public string Key => "3";
    public string Description => "Buscar repuestos";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.WriteLine("Criterio (nombre/categoria/stockmax):");
        var criterion = Console.ReadLine() ?? string.Empty;
        Console.Write("Valor: ");
        var value = Console.ReadLine() ?? string.Empty;

        var result = await context.InventoryService.SearchAsync(criterion, value);
        Console.WriteLine("\n=== Resultado búsqueda ===");
        foreach (var part in result)
        {
            Console.WriteLine($"{part.Id} | {part.Name} | Stock:{part.Stock} | ${part.UnitPrice}");
        }
    }
}
