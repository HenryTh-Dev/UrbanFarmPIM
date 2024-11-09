using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;

public class FarmContext : DbContext
{
    public DbSet<Farm> Farms { get; set; }
    public DbSet<PlantingArea> PlantingAreas { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<Planting> Plantings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=farm.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Resource)
            .WithMany()
            .HasForeignKey(si => si.ResourceId);

        modelBuilder.Entity<Planting>()
            .HasOne(p => p.Resource)
            .WithMany(r => r.Plantings)
            .HasForeignKey(p => p.ResourceId);

        modelBuilder.Entity<Planting>()
            .HasMany(p => p.Employees);
    }
}