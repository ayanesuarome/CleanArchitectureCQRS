using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveTypes;
using CleanArch.Persistence.EntityConfigurations.Read;
using CleanArch.Persistence.EntityConfigurations.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArch.Persistence;

internal sealed partial class CleanArchEFWriteDbContext : DbContext, IWriteUnitOfWork
{
    public const string ConnectionStringName = "CleanArchSqlServerDbContext";

    public CleanArchEFWriteDbContext(DbContextOptions<CleanArchEFWriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CleanArchEFWriteDbContext).Assembly,
            type => WriteConfigurationsFilter(type));

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Saves all of the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Database.BeginTransactionAsync(cancellationToken);

    private static bool WriteConfigurationsFilter(Type type) =>
        type.GetInterface(nameof(IWriteConfiguration)) != null;
}