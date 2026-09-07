using TechPartsHub.ConsoleApp.Commands;

namespace TechPartsHub.ConsoleApp.Factories;

// PATRÓN SIMPLE FACTORY: centraliza creación/registro de comandos del menú.
public static class CommandFactory
{
    public static IReadOnlyDictionary<string, IMenuCommand> CreateAll()
    {
        IMenuCommand[] commands =
        [
            new ViewInventoryCommand(),
            new RegisterSparePartCommand(),
            new SearchSparePartsCommand(),
            new SortSparePartsCommand(),
            new ViewLowStockCommand(),
            new CreateOrderCommand(),
            new AddOrderItemCommand(),
            new RemoveOrderItemCommand(),
            new UndoOrderItemsChangeCommand(),
            new EnqueueOrderCommand(),
            new ProcessNextOrderCommand(),
            new ViewOrdersCommand(),
            new CancelOrderCommand(),
            new GenerateInvoiceCommand(),
            new ViewInvoicesCommand(),
            new PayInvoiceCommand(),
            new ViewPaymentsCommand(),
            new ExitCommand()
        ];

        return commands.ToDictionary(x => x.Key);
    }
}
