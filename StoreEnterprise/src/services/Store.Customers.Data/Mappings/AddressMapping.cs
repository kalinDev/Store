using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Customers.Domain.Entities;

namespace Store.Customers.Data.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.City)
            .IsRequired()
            .HasColumnType("varchar(100)");
        
        builder.Property(a => a.Complement)
            .IsRequired()
            .HasColumnType("varchar(250)");
        
        builder.Property(a => a.Neighborhood)
            .IsRequired()
            .HasColumnType("varchar(100)");
        
        builder.Property(a => a.Number)
            .IsRequired()
            .HasColumnType("varchar(50)");
        
        builder.Property(a => a.State)
            .IsRequired()
            .HasColumnType("varchar(50)");
        
        builder.Property(a => a.Street)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(a => a.ZipCode)
            .IsRequired()
            .HasColumnType("varchar(20)");
    }
}