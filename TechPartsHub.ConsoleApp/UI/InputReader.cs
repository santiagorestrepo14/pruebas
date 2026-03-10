using System.Globalization;

namespace TechPartsHub.ConsoleApp.UI;

public static class InputReader
{
    public static Guid ReadGuid(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine();
            if (Guid.TryParse(raw, out var id))
                return id;

            Console.WriteLine("Valor inválido. Debe ingresar un GUID válido.");
        }
    }

    public static int ReadInt(string prompt, int minValue = int.MinValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine();
            if (int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value) && value >= minValue)
                return value;

            Console.WriteLine($"Valor inválido. Debe ingresar un entero >= {minValue}.");
        }
    }

    public static decimal ReadDecimal(string prompt, decimal minValue = decimal.MinValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine();
            if (decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value) && value >= minValue)
                return value;

            Console.WriteLine($"Valor inválido. Debe ingresar un decimal >= {minValue} usando formato invariante (ej: 123.45).");
        }
    }

    public static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(raw))
                return raw;

            Console.WriteLine("El valor no puede estar vacío.");
        }
    }
}
