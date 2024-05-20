using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Entities;

/// <summary>
/// Represents the link between a role and a permission.
/// </summary>
public class RolePermission
{
    public RolePermission(Guid roleId, PermissionId permissionId)
    {
        Ensure.NotEmpty(roleId, "The role ID is required", nameof(roleId));
        Ensure.NotEmpty(permissionId, "The permission ID is required", nameof(permissionId));

        RoleId = roleId;
        PermissionId = permissionId;
    }

    /// <summary>
    /// Initializes a new instance of the class <see cref="RolePermission"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private RolePermission()
    {
    }

    /// <summary>
    /// Gets or sets the primary key of the role that is linked to the user.
    /// </summary>
    public Guid RoleId { get; init; }

    /// <summary>
    /// Gets or sets the primary key of the permission that is linked to the user.
    /// </summary>
    public PermissionId PermissionId { get; init; }
}
