using CleanArch.Application.Interfaces.Identity;
using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.DatabaseContext;

public sealed partial class CleanArchEFDbContext : DbContext
{
    private readonly IUserService _userService;

    public CleanArchEFDbContext(DbContextOptions<CleanArchEFDbContext> options, IUserService userService)
        : base(options)
    {
        _userService = userService;
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            DateTime now = DateTime.Now;
            entry.Entity.DateModified = now;
            //entry.Entity.ModifiedBy = _userService.UserId;

            if(entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = now;
                //entry.Entity.CreatedBy = _userService.UserId;
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