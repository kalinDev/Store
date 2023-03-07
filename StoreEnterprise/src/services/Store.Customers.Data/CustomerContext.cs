using Core.DomainObjects.Interfaces;
using Core.MediatR;
using Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Store.Customers.Data.Extensions;
using Store.Customers.Domain.Entities;

namespace Store.Customers.Data;


public sealed class CustomersContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public CustomersContext(DbContextOptions<CustomersContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<ValidationResult>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersContext).Assembly);
    }

    public async Task<bool> Commit()
    {
       var success = await SaveChangesAsync() > 0;
       if (success) await _mediatorHandler.PublishEvents(this);
       
       return success;
    }
}