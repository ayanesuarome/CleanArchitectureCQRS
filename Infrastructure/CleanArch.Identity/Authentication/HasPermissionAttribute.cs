using CleanArch.Domain.Enumerations;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Identity.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission)
        : base(policy: permission.ToString())
    {
    }
}
