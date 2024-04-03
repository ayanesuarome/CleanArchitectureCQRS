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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchEFDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}