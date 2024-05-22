using MediatR;
using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Authentication;

namespace CleanArch.Api.Features.LeaveTypes;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(Permissions.AccessLeaveTypes)]
public sealed partial class LeaveTypesController(ISender sender, IPublisher publisher)
    : BaseController(sender, publisher)
{
}
