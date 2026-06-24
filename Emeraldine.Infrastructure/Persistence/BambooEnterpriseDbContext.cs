using Microsoft.EntityFrameworkCore;
using Emeraldine.Domain.Entities;

namespace Emeraldine.Infrastructure.Persistence;

public class BambooEnterpriseDbContext : DbContext
{
    public BambooEnterpriseDbContext(DbContextOptions<BambooEnterpriseDbContext> options)
        : base(options)
    {
    }

    public DbSet<BambooSpecies> BambooSpecies => Set<BambooSpecies>();
    public DbSet<InventoryBatch> InventoryBatches => Set<InventoryBatch>();
    public DbSet<PlantUnit> PlantUnits => Set<PlantUnit>();
    public DbSet<Culm> Culms => Set<Culm>();
    public DbSet<Harvest> Harvests => Set<Harvest>();
    public DbSet<MaterialBatch> MaterialBatches => Set<MaterialBatch>();

    public DbSet<Product> Products => Set<Product>();
    public DbSet<StockItem> StockItems => Set<StockItem>();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<BambooTechnicalProfile> BambooTechnicalProfiles => Set<BambooTechnicalProfile>();
    public DbSet<BambooManagementWindow> BambooManagementWindows => Set<BambooManagementWindow>();

    public DbSet<PlantVariant> PlantVariants => Set<PlantVariant>();
    public DbSet<ShippingProfile> ShippingProfiles => Set<ShippingProfile>();

    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
}
