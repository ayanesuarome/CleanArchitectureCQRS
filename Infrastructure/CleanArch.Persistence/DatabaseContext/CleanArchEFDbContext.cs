using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.DatabaseContext;

public partial class CleanArchEFDbContext : DbContext
{
    public CleanArchEFDbContext(DbContextOptions<CleanArchEFDbContext> options) : base(options)
    {            
    }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }
    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
    public virtual DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            DateTime now = DateTime.Now;
            entry.Entity.DateModified = now;

            if(entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchEFDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
