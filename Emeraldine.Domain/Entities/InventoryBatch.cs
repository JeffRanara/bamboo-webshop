using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class InventoryBatch
{
    public Guid Id { get; set; }

    // Link to Species
    public Guid BambooSpeciesId { get; set; }
    public BambooSpecies BambooSpecies { get; set; } = null!;

    // Acquisition Details
    public DateTime PurchaseDate { get; set; }
    public string SupplierName { get; set; } = string.Empty;

    // Physical Characteristics
    public double PotVolumeLiters { get; set; }

    // Quantity Tracking
    public int Quantity { get; set; }

    // Cost Tracking
    public decimal UnitCost { get; set; }
    public Currency Currency { get; set; }
    public decimal ExchangeRateToSek { get; set; }

    // Audit Trail
    public string? UpdateReason { get; set; }
    public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

    // Calculated (not stored in DB later if we choose)
    public decimal TotalCostSek => UnitCost * Quantity * ExchangeRateToSek;
}