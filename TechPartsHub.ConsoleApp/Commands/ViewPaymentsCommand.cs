using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ViewPaymentsCommand : IMenuCommand
{
    public string Key => "17";
    public string Description => "Ver pagos";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var payments = await context.PaymentService.GetPaymentsAsync();
        Console.WriteLine("\n=== Pagos ===");
        foreach (var payment in payments)
            Console.WriteLine($"{payment.Id} | Factura:{payment.InvoiceId} | Monto:${payment.Amount} | Método:{payment.Method} | TX:{payment.TransactionCode}");
    }
}
