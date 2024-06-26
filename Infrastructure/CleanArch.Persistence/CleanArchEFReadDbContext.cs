using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Persistence.EntityConfigurations.Read;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence;

internal sealed partial class CleanArchEFReadDbContext : DbContext, IReadUnitOfWork
{
    public const string ConnectionStringName = "CleanArchSqlServerDbContext";

    public CleanArchEFReadDbContext(DbContextOptions<CleanArchEFReadDbContext> options)
        : base(options)
    {
    }

    public DbSet<LeaveRequestSummary> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CleanArchEFReadDbContext).Assembly,
            type => ReadConfigurationsFilter(type));

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

    private static bool ReadConfigurationsFilter(Type type) =>
        type.GetInterface(nameof(IReadConfiguration)) != null;
}