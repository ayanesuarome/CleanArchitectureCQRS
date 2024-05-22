using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Authentication;
using CleanArch.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(Permissions.AccessLeaveAllocations)]
public sealed partial class LeaveAllocationController(ISender mediator, IPublisher publisher)
    : BaseController(mediator, publisher)
{
}
