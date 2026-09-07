using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class GenerateInvoiceCommand : IMenuCommand
{
    public string Key => "14";
    public string Description => "Generar factura";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var orderId = InputReader.ReadGuid("Id Pedido: ");

        var invoice = await context.BillingService.GenerateInvoiceAsync(orderId);
        Console.WriteLine($"Factura generada: {invoice.Id} | Total:{invoice.Total}");
    }
}
