using CleanArch.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Identity.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permission)
        : base(policy: permission.ToString())
    {
    }
}
