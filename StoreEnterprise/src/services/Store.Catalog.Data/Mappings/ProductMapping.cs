using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Catalog.Domain.Entities;

namespace Store.Catalog.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");
        
        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(250)");
        
        builder.Property(c => c.ImageUrl)
            .IsRequired()
            .HasColumnType("varchar(250)");
    }
}