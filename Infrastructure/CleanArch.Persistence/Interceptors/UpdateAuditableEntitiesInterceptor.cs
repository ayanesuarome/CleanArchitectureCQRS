using CleanArch.Application.Interfaces.Identity;
using CleanArch.Domain.Primitives;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Persistence.Interceptors;

internal sealed class UpdateAuditableEntitiesInterceptor(IServiceScopeFactory serviceScopeFactory) : SaveChangesInterceptor
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

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IUserService userService = scope
            .ServiceProvider
            .GetRequiredService<IUserService>();

        IEnumerable<EntityEntry<IAuditableEntity>> entries = eventData
            .Context
            .ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        DateTimeOffset now = DateTimeOffset.Now;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.DateCreated), now);
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.CreatedBy), userService.UserId);
            }
            else
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.DateModified), now);
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.ModifiedBy), userService.UserId);
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SetCurrentPropertyValue(
        EntityEntry entry,
        string propertyName,
        DateTimeOffset now) =>
        entry.Property(propertyName).CurrentValue = now;

    private static void SetCurrentPropertyValue(
        EntityEntry entry,
        string propertyName,
        string userId) =>
        entry.Property(propertyName).CurrentValue = userId;
}
