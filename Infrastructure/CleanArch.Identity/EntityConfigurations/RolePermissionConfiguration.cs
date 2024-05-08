using CleanArch.Domain.Entities;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission<int>>
{
    public void Configure(EntityTypeBuilder<RolePermission<int>> builder)
    {
        builder.ToTable(TableNames.RolePermissions);

        // compound key
        builder.HasKey(rolePermissions =>
            new {
                rolePermissions.RoleId,
                rolePermissions.PermissionId
            });

        builder.HasData(
            Create(Role.Registered, Domain.Enums.Permission.AccessLeaveTypes),
            #region Employee
            #endregion
            #region Administrator
            Create(Role.Administrator, Domain.Enums.Permission.CreateLeaveType),
            Create(Role.Administrator, Domain.Enums.Permission.UpdateLeaveType),
            Create(Role.Administrator, Domain.Enums.Permission.DeleteLeaveType)
            #endregion
            );
    }

    private static RolePermission<int> Create(Role role, Domain.Enums.Permission permission)
    {
        return new RolePermission<int>(
            roleId: role.Value,
            permissionId: (int)permission);
    }
}
