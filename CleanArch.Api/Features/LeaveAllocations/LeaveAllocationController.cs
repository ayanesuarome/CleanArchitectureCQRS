using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(Permission.AccessLeaveAllocations)]
public sealed partial class LeaveAllocationController(ISender mediator, IPublisher publisher)
    : BaseController(mediator, publisher)
{
}
