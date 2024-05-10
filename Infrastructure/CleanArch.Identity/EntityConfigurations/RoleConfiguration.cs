using CleanArch.Domain.Entities;
using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(role => role.Users)
            .WithMany(user => user.Roles);

        IEnumerable<Role> roles = Domain.Enumerations.Role
            .GetValues()
            .Select(role => new Role(role.Id, role.Name)
            {
                NormalizedName = role.Name.ToUpper()
            });

        builder.HasData(roles);
    }
}
