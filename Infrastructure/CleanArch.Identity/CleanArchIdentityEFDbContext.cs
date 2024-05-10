using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArch.Identity;

public sealed partial class CleanArchIdentityEFDbContext : IdentityDbContext<User, Role, Guid>
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
