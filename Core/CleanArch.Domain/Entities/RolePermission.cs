using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Entities;

/// <summary>
/// Represents the link between a role and a permission.
/// </summary>
/// <typeparam name="TKey">The type of the primary key used for roles and permissions.</typeparam>
public class RolePermission<TKey>
    where TKey : IEquatable<TKey>
{
    public RolePermission(TKey roleId, TKey permissionId)
    {
        Ensure.NotEmpty(roleId, "The role ID is required", nameof(roleId));
        Ensure.NotEmpty(permissionId, "The permission ID is required", nameof(permissionId));

        RoleId = roleId;
        PermissionId = permissionId;
    }

    /// <summary>
    /// Initializes a new instance of the class <see cref="RolePermission{TKey}"/>.
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
    public TKey RoleId { get; init; }

    /// <summary>
    /// Gets or sets the primary key of the permission that is linked to the user.
    /// </summary>
    public TKey PermissionId { get; init; }
}
