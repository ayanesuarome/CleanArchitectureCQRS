using CleanArch.Domain.Authentication;
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

        IEnumerable<Role> roles = Roles
            .GetValues()
            .Select(role => new Role(role.Id, role.Name)
            {
                NormalizedName = role.Name.ToUpper()
            });

        builder.HasData(roles);
    }
}
