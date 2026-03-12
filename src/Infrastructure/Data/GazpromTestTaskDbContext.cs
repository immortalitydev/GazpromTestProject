using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GazpromTestTaskDbContext(DbContextOptions<GazpromTestTaskDbContext> options) : DbContext(options)
{
    public DbSet<Offer> Offers { get; set; } = null!;
    
    public DbSet<Supplier> Suppliers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GazpromTestTaskDbContext).Assembly);
    }
}
