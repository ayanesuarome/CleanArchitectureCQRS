using CleanArch.Domain.Entities;
using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Role = CleanArch.Domain.Enumerations.Role;
using Permission = CleanArch.Domain.Enumerations.Permission;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission<int>>
{
    public void Configure(EntityTypeBuilder<RolePermission<int>> builder)
    {
        builder.ToTable(TableNames.RolePermissions);

        // composite key
        builder.HasKey(rolePermissions =>
            new {
                rolePermissions.RoleId,
                rolePermissions.PermissionId
            });

        builder.HasData(
            Create(Role.Registered, Permission.AccessLeaveTypes),
            #region Employee
            #endregion
            #region Administrator
            Create(Role.Administrator, Permission.CreateLeaveType),
            Create(Role.Administrator, Permission.UpdateLeaveType),
            Create(Role.Administrator, Permission.DeleteLeaveType)
            #endregion
            );
    }

    private static RolePermission<int> Create(Role role, Permission permission)
    {
        return new RolePermission<int>(
            roleId: role.Id,
            permissionId: (int)permission);
    }
}
