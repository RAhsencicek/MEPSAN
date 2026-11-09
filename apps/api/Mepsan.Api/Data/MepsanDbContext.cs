using Microsoft.EntityFrameworkCore;
using Mepsan.Api.Models;

namespace Mepsan.Api.Data;

public class MepsanDbContext : DbContext
{
    public MepsanDbContext(DbContextOptions<MepsanDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<StockLocation> StockLocations => Set<StockLocation>();
    public DbSet<LocationInventory> LocationInventories => Set<LocationInventory>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<Technician> Technicians => Set<Technician>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<StockLocation>().HasKey(l => l.Id);
        modelBuilder.Entity<LocationInventory>().HasKey(i => i.Id);
        modelBuilder.Entity<StockMovement>().HasKey(m => m.Id);
        modelBuilder.Entity<WorkOrder>().HasKey(w => w.Id);
        modelBuilder.Entity<Technician>().HasKey(t => t.Id);
    }
}
