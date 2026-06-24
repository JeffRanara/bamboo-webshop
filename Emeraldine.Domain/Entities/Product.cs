using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    // Basic info
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Classification
    public ProductCategory Category { get; set; }

    // Source (important for traceability)
    public ProductSourceType SourceType { get; set; }

    // Optional links (depending on source)
    public Guid? InventoryBatchId { get; set; }
    public InventoryBatch? InventoryBatch { get; set; }

    public Guid? MaterialBatchId { get; set; }
    public MaterialBatch? MaterialBatch { get; set; }

    // Pricing
    public decimal Price { get; set; }
    public Currency Currency { get; set; }

    // Display / webshop
    public string? SKU { get; set; }
    public bool IsActive { get; set; } = true;

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}