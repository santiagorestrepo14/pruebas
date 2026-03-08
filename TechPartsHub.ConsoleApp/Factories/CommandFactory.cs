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
            new EnqueueOrderCommand(),
            new ProcessNextOrderCommand(),
            new ViewOrdersCommand(),
            new GenerateInvoiceCommand(),
            new ViewInvoicesCommand(),
            new ExitCommand()
        ];

        return commands.ToDictionary(x => x.Key);
    }
}
