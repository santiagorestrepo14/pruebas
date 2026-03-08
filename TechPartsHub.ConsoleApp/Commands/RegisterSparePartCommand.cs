using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class RegisterSparePartCommand : IMenuCommand
{
    public string Key => "2";
    public string Description => "Registrar repuesto";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.Write("SKU: ");
        var sku = Console.ReadLine() ?? string.Empty;
        Console.Write("Nombre: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Categoría: ");
        var category = Console.ReadLine() ?? string.Empty;
        Console.Write("Precio unitario: ");
        var price = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("Stock inicial: ");
        var stock = int.Parse(Console.ReadLine() ?? "0");

        var part = await context.InventoryService.RegisterSparePartAsync(sku, name, category, price, stock);
        Console.WriteLine($"Repuesto creado: {part.Id}");
    }
}
