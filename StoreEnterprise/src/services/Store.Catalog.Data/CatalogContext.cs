using Core.DomainObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using Store.Catalog.Domain.Entities;

namespace Store.Catalog.Data;

public class CatalogContext : DbContext, IUnitOfWork
{
    public DbSet<Product> Products { get; set; }

    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
            
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }
    
    public async Task<bool> Commit()
        => await base.SaveChangesAsync() > 0;
}