using CleanArch.Domain.Authentication;
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

        builder.Property(permission => permission.Id)
            .HasConversion(
                permissionId => permissionId.Id,
                id => new PermissionId(id));

        IEnumerable<Permission> permissions = Permissions
            .GetValues()
            .Select(permission => new Permission(
                id: permission.Value,
                name: permission.Name,
                description: permission.Description));

        builder.HasData(permissions);
    }
}
