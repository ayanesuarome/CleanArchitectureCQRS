using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Time;

namespace CleanArch.Persistence.Interceptors;

internal sealed class SoftDeleteEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<ISoftDeletableEntity>> entries = eventData
            .Context
            .ChangeTracker
            .Entries<ISoftDeletableEntity>()
            .Where(entry => entry.State == EntityState.Deleted);

        foreach (var softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Property(property => property.IsDeleted).CurrentValue = true;
            softDeletable.Property(property => property.DeletedOn).CurrentValue = SystemTimeProvider.UtcNow;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
