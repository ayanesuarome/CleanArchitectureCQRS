using CleanArch.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Identity.DatabaseContext;

public sealed partial class CleanArchIdentityEFDbContext : IdentityDbContext<ApplicationUser>
{
    public CleanArchIdentityEFDbContext(DbContextOptions<CleanArchIdentityEFDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(CleanArchIdentityEFDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
