using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArch.Domain.Authentication;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermissions);

        // composite key
        builder.HasKey(rolePermissions =>
            new {
                rolePermissions.RoleId,
                rolePermissions.PermissionId
            });

        builder.Property(rolePermissions => rolePermissions.PermissionId)
            .HasConversion(
                permissionId => permissionId.Id,
                id => new PermissionId(id));

        builder.HasData(
        #region Employee
            Create(Roles.Employee, Permissions.AccessLeaveTypes),
            Create(Roles.Employee, Permissions.AccessLeaveRequests),
            Create(Roles.Employee, Permissions.CreateLeaveRequest),
            Create(Roles.Employee, Permissions.UpdateLeaveRequest),
            Create(Roles.Employee, Permissions.CancelLeaveRequest),
        #endregion
        #region Administrator
            Create(Roles.Administrator, Permissions.AccessLeaveTypes),
            Create(Roles.Administrator, Permissions.CreateLeaveType),
            Create(Roles.Administrator, Permissions.UpdateLeaveType),
            Create(Roles.Administrator, Permissions.DeleteLeaveType),
            Create(Roles.Administrator, Permissions.AccessLeaveRequests),
            Create(Roles.Administrator, Permissions.DeleteLeaveRequest),
            Create(Roles.Administrator, Permissions.ChangeLeaveRequestApproval),
            Create(Roles.Administrator, Permissions.AccessLeaveAllocations),
            Create(Roles.Administrator, Permissions.CreateLeaveAllocation)
        #endregion
            );
    }

    private static RolePermission Create(Roles role, Permissions permission)
    {
        return new RolePermission(
            roleId: role.Id,
            permissionId: new PermissionId(permission.Value));
    }
}
