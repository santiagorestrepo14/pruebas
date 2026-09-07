using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class SearchSparePartsCommand : IMenuCommand
{
    public string Key => "3";
    public string Description => "Buscar repuestos";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.WriteLine("Criterio (nombre/categoria/stockmax/compuesto):");
        Console.WriteLine("Para compuesto use: nombre:ryzen;categoria:CPU;stockmax:10");
        var criterion = InputReader.ReadRequiredText("Criterio: ");
        var value = InputReader.ReadRequiredText("Valor: ");

        var result = await context.InventoryService.SearchAsync(criterion, value);
        Console.WriteLine("\n=== Resultado búsqueda ===");
        foreach (var part in result)
            Console.WriteLine($"{part.Id} | {part.Name} | Stock:{part.Stock} | ${part.UnitPrice}");
    }
}
