using TechPartsHub.Domain.Exceptions;

namespace TechPartsHub.Domain.Entities;

public sealed class SparePart
{
    public Guid Id { get; }
    public string Sku { get; }
    public string Name { get; }
    public string Category { get; }
    public decimal UnitPrice { get; }
    public int Stock { get; private set; }

    public SparePart(Guid id, string sku, string name, string category, decimal unitPrice, int stock)
    {
        if (id == Guid.Empty) throw new DomainException("El Id del repuesto es inválido.");
        if (string.IsNullOrWhiteSpace(sku)) throw new DomainException("El SKU es obligatorio.");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("El nombre del repuesto es obligatorio.");
        if (string.IsNullOrWhiteSpace(category)) throw new DomainException("La categoría es obligatoria.");
        if (unitPrice <= 0) throw new DomainException("El precio unitario debe ser mayor a cero.");
        if (stock < 0) throw new DomainException("No se puede registrar stock negativo.");

        Id = id;
        Sku = sku.Trim();
        Name = name.Trim();
        Category = category.Trim();
        UnitPrice = unitPrice;
        Stock = stock;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0) throw new DomainException("La cantidad a descontar debe ser mayor a cero.");
        if (quantity > Stock) throw new DomainException($"Stock insuficiente para {Name}. Solicitado: {quantity}, disponible: {Stock}.");
        Stock -= quantity;
    }
}
