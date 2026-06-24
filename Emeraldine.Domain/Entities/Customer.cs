namespace Emeraldine.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }

    // Basic identity
    public string Name { get; set; } = string.Empty;

    // Contact info
    public string? Email { get; set; }
    public string? Phone { get; set; }

    // Address (simple for now)
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    // Notes
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}