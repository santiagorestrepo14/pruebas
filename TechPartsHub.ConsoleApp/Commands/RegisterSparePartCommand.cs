using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class RegisterSparePartCommand : IMenuCommand
{
    public string Key => "2";
    public string Description => "Registrar repuesto";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var sku = InputReader.ReadRequiredText("SKU: ");
        var name = InputReader.ReadRequiredText("Nombre: ");
        var category = InputReader.ReadRequiredText("Categoría: ");
        var price = InputReader.ReadDecimal("Precio unitario (ej. 99.90): ", 0.01m);
        var stock = InputReader.ReadInt("Stock inicial: ", 0);

        var part = await context.InventoryService.RegisterSparePartAsync(sku, name, category, price, stock);
        Console.WriteLine($"Repuesto creado: {part.Id}");
    }
}
