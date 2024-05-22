﻿using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Authentication;
using CleanArch.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[HasPermission(Permissions.AccessLeaveRequests)]
public sealed partial class LeaveRequestController(ISender mediator, IPublisher publisher)
    : BaseController(mediator, publisher)
{
}
