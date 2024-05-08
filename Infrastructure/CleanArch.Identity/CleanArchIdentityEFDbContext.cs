using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Identity;

public sealed partial class CleanArchIdentityEFDbContext : IdentityDbContext<User>
{
    public CleanArchIdentityEFDbContext(DbContextOptions<CleanArchIdentityEFDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CleanArchIdentityEFDbContext).Assembly);
        builder.HasDefaultSchema("identity");
    }
}
