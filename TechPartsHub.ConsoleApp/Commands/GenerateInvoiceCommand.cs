using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class GenerateInvoiceCommand : IMenuCommand
{
    public string Key => "12";
    public string Description => "Generar factura";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        Console.Write("Id Pedido: ");
        var orderId = Guid.Parse(Console.ReadLine() ?? string.Empty);

        var invoice = await context.BillingService.GenerateInvoiceAsync(orderId);
        Console.WriteLine($"Factura generada: {invoice.Id} | Total:{invoice.Total}");
    }
}
