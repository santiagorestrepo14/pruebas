using TechPartsHub.Application.Abstractions.Repositories;
using TechPartsHub.Domain.Entities;

namespace TechPartsHub.Infrastructure.Seed;

public static class SeedData
{
    public static async Task PopulateAsync(ISparePartRepository repository, CancellationToken cancellationToken = default)
    {
        var current = await repository.GetAllAsync(cancellationToken);
        if (current.Count > 0) return;

        var seed = new[]
        {
            new SparePart(Guid.NewGuid(), "CPU-001", "Procesador Ryzen 7", "CPU", 320m, 10),
            new SparePart(Guid.NewGuid(), "RAM-016", "Memoria RAM 16GB DDR5", "RAM", 90m, 30),
            new SparePart(Guid.NewGuid(), "SSD-1TB", "SSD NVMe 1TB", "Storage", 110m, 15),
            new SparePart(Guid.NewGuid(), "PSU-750", "Fuente 750W Gold", "Power", 140m, 6),
            new SparePart(Guid.NewGuid(), "GPU-4070", "Placa de Video RTX 4070", "GPU", 640m, 4),
            new SparePart(Guid.NewGuid(), "FAN-120", "Cooler 120mm", "Cooling", 15m, 45)
        };

        foreach (var part in seed)
            await repository.AddAsync(part, cancellationToken);
    }
}
