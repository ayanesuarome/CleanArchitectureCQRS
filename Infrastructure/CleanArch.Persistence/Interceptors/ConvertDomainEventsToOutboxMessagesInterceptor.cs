using CleanArch.Domain.Core.Primitives;
using CleanArch.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace CleanArch.Persistence.Interceptors;

internal sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        List<EntityEntry<IAggregateRoot>> aggregateRoots = dbContext
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .ToList();

        List<IDomainEvent> domainEvents = aggregateRoots
            .SelectMany(entityEntry => entityEntry.Entity.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        OutboxMessage[] events = domainEvents
            .Select(domainEvent => new OutboxMessage(
                id: domainEvent.Id,
                type: domainEvent.GetType().Name,
                content: JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    }),
                occurredOn: domainEvent.OcurredOn)
            ).ToArray();

        dbContext.Set<OutboxMessage>().AddRange(events);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
