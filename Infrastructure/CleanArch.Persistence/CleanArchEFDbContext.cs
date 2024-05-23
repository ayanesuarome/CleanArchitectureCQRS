using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace CleanArch.Persistence;

public sealed partial class CleanArchEFDbContext : DbContext, IUnitOfWork
{
    public CleanArchEFDbContext(DbContextOptions<CleanArchEFDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    private readonly IPublisher _publisher;

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchEFDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Saves all of the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        
        await PublishDomainEvents(cancellationToken);
       
        return result;
    }

    /// <inheritdoc />
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Database.BeginTransactionAsync(cancellationToken);

    /// <summary>
    /// Publishes and then clears all the domain events that exist within the current transaction.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        List<EntityEntry<IAggregateRoot>> aggregateRoots = ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .ToList();

        List<IDomainEvent> domainEvents = aggregateRoots
            .SelectMany(entityEntry => entityEntry.Entity.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        IEnumerable<Task> tasks = domainEvents.Select(domainEvent => _publisher.Publish(domainEvent));

        await Task.WhenAll(tasks);
    }
}