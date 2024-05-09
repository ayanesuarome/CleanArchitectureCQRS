using CleanArch.Domain.Entities;
using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(permission => permission.Id);

        IEnumerable<Permission> permissions = Enum
            .GetValues<Domain.Enumerations.Permission>()
            .Select(permission => new Permission(
                id: (int)permission,
                name: permission.ToString()));

        builder.HasData(permissions);
    }
}
