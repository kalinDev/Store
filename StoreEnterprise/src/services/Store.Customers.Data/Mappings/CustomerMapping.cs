using Core.DomainObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Customers.Domain.Entities;

namespace Store.Customers.Data.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.OwnsOne(c => c.Cpf, tf =>
        {
            tf.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
        });
        
        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(Email.EmailMaxLength)
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EmailMaxLength})");
        });

        builder.HasOne(c => c.Address)
            .WithOne(a => a.Customer)
            .HasForeignKey<Address>(a => a.CustomerId);
    }
}