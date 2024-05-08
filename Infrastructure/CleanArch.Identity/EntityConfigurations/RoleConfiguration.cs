using CleanArch.Domain.Entities;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(role => role.Value);

        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission<int>>();

        builder.HasMany(role => role.Users)
            .WithMany();

        builder.HasData(Role.List);
    }
}
