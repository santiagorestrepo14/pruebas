using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ViewInventoryCommand : IMenuCommand
{
    public string Key => "1";
    public string Description => "Ver inventario";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var parts = await context.InventoryService.GetAllAsync();
        Console.WriteLine("\n=== Inventario ===");
        foreach (var part in parts)
        {
            Console.WriteLine($"{part.Id} | {part.Sku} | {part.Name} | Cat:{part.Category} | ${part.UnitPrice} | Stock:{part.Stock}");
        }
    }
}
