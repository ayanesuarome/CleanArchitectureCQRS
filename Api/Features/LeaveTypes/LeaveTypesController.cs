using MediatR;
using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(LeaveTypePermissions.AccessLeaveTypes)]
public sealed partial class LeaveTypesController(ISender sender) : BaseController(sender);
