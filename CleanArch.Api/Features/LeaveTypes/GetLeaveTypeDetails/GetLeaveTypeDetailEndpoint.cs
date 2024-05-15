﻿using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Enumerations;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet(ApiRoutes.LeaveTypes.GetById)]
    [ProducesResponseType(typeof(LeaveTypeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(Permission.AccessLeaveTypes)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<LeaveTypeDetailDto> result = await Sender.Send(new GetLeaveTypeDetail.Query(id), cancellationToken);

        return result.Match(
            onSuccess: value => Ok(value),
            onFailure: () => NotFound());
    }
}
