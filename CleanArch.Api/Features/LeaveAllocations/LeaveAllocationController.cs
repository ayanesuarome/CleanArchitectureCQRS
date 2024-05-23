using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(LeaveAllocationPermissions.AccessLeaveAllocations)]
public sealed partial class LeaveAllocationController(ISender sender) : BaseController(sender);
