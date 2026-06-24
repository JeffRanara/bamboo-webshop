namespace Emeraldine.Domain.Entities;

public class Cart
{
    public Guid Id { get; set; }

    public string SessionId { get; set; } = null!;

    public List<CartItem> Items { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
