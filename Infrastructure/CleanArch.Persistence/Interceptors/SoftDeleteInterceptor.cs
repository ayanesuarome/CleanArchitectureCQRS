using CleanArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArch.Persistence.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if(eventData is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<ISoftDeletable>> entries = eventData
            .Context
            .ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Entity.IsDeleted = true;
            softDeletable.Entity.DeletedOn = DateTimeOffset.Now;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
