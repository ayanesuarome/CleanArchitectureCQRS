using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Identity.Authentication;

/// <summary>
/// Specifies that the method that this attribute is applied to requires the specified permission.
/// </summary>
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission)
    {
    }
}
