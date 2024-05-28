using CleanArch.Domain.Core.Primitives;
using CleanArch.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace CleanArch.Persistence.Interceptors
{
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

            OutboxMessage[] events = dbContext
                .ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
                .Select(entityEntry => entityEntry.Entity)
                .SelectMany(aggregateRoot =>
                {
                    IReadOnlyCollection<IDomainEvent> domainEvents = aggregateRoot.DomainEvents;
                    aggregateRoot.ClearDomainEvents();
                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = domainEvent.Id,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                        }),
                    OcurredOn = domainEvent.OcurredOn
                })
                .ToArray();

            dbContext.Set<OutboxMessage>().AddRange(events);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
