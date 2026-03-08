using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

// PATRÓN COMMAND: encapsula cada acción del menú.
public interface IMenuCommand
{
    string Key { get; }
    string Description { get; }
    Task ExecuteAsync(ApplicationContext context);
}
