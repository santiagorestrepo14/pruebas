using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ViewInvoicesCommand : IMenuCommand
{
    public string Key => "13";
    public string Description => "Ver facturas";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var invoices = await context.BillingService.GetInvoicesAsync();
        Console.WriteLine("\n=== Facturas ===");
        foreach (var invoice in invoices)
        {
            Console.WriteLine($"{invoice.Id} | Pedido:{invoice.OrderId} | Fecha:{invoice.IssuedAtUtc:u} | Total:${invoice.Total}");
        }
    }
}
