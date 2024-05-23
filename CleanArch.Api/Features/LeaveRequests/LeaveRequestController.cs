using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(LeaveRequestPermissions.AccessLeaveRequests)]
public sealed partial class LeaveRequestController(ISender sender) : BaseController(sender);
