﻿using CleanArch.Application.Abstractions.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Time;

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

        IEnumerable<EntityEntry<IAuditableEntity>> entries = eventData
            .Context
            .ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        if(!entries.Any())
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IUserIdentifierProvider userIdentifierProvider = scope
            .ServiceProvider
            .GetRequiredService<IUserIdentifierProvider>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.DateCreated), SystemTimeProvider.UtcNow);
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.CreatedBy), userIdentifierProvider.UserId);
            }
            else
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.DateModified), SystemTimeProvider.UtcNow);
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.ModifiedBy), userIdentifierProvider.UserId);
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
        Guid userId) =>
        entry.Property(propertyName).CurrentValue = userId;
}
