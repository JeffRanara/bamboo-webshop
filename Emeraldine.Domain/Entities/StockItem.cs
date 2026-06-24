namespace Emeraldine.Domain.Entities;

public class StockItem
{
    public Guid Id { get; set; }

    // Link to product
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    // Quantity available for sale
    public int QuantityAvailable { get; set; }

    // Optional reserved quantity (future use)
    public int QuantityReserved { get; set; }

    // Optional stock tracking notes
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}