using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class PayInvoiceCommand : IMenuCommand
{
    public string Key => "16";
    public string Description => "Pagar factura";

    public async Task ExecuteAsync(ApplicationContext context)
    {
        var invoiceId = InputReader.ReadGuid("Id Factura: ");
        var method = InputReader.ReadRequiredText("Método (tarjeta/efectivo/transferencia): ");

        var payment = await context.PaymentService.PayInvoiceAsync(invoiceId, method);
        Console.WriteLine($"Pago registrado: {payment.Id} | Método:{payment.Method} | TX:{payment.TransactionCode}");
    }
}
