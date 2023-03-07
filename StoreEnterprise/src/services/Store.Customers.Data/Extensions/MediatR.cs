using Core.DomainObjects.Entities;
using Core.MediatR;
using Microsoft.EntityFrameworkCore;

namespace Store.Customers.Data.Extensions;

public static class MediatR
{
    public static async Task PublishEvents<T>(this IMediatorHandler mediatorHandler, T context) where T : DbContext
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications?.Any() == true);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entityItem => entityItem.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(mediatorHandler.PublishEvent)
            .ToList();

        await Task.WhenAll(tasks);
    }
}