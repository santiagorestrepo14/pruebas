using TechPartsHub.ConsoleApp.UI;

namespace TechPartsHub.ConsoleApp.Commands;

public sealed class ExitCommand : IMenuCommand
{
    public string Key => "0";
    public string Description => "Salir";

    public Task ExecuteAsync(ApplicationContext context)
    {
        Console.WriteLine("Saliendo de TechPartsHub...");
        return Task.CompletedTask;
    }
}
