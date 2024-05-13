using CleanArch.Domain.Entities;
using CleanArch.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Role = CleanArch.Domain.Enumerations.Role;
using Permission = CleanArch.Domain.Enumerations.Permission;

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

        builder.HasData(
        #region Employee
            Create(Role.Employee, Permission.AccessLeaveTypes),
            Create(Role.Employee, Permission.AccessLeaveRequests),
            Create(Role.Employee, Permission.CreateLeaveRequest),
            Create(Role.Employee, Permission.UpdateLeaveRequest),
            Create(Role.Employee, Permission.CancelLeaveRequest),
        #endregion
        #region Administrator
            Create(Role.Administrator, Permission.AccessLeaveTypes),
            Create(Role.Administrator, Permission.CreateLeaveType),
            Create(Role.Administrator, Permission.UpdateLeaveType),
            Create(Role.Administrator, Permission.DeleteLeaveType),
            Create(Role.Administrator, Permission.AccessLeaveRequests),
            Create(Role.Administrator, Permission.DeleteLeaveRequest),
            Create(Role.Administrator, Permission.ChangeLeaveRequestApproval),
            Create(Role.Administrator, Permission.AccessLeaveAllocations),
            Create(Role.Administrator, Permission.CreateLeaveAllocation)
        #endregion
            );
    }

    private static RolePermission Create(Role role, Permission permission)
    {
        return new RolePermission(
            roleId: role.Id,
            permissionId: (int)permission);
    }
}
