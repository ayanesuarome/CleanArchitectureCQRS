using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Identity.Authentication;

/// <summary>
/// Represents an authorization requirement implementation.
/// </summary>
public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission) => Permission = permission;

    public string Permission { get; init; }
}
