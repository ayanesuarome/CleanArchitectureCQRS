﻿using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET api/<v>/leave-requests/5
    [HttpGet(ApiRoutes.LeaveRequests.GetById)]
    [ProducesResponseType(typeof(GetLeaveRequestDetail.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<GetLeaveRequestDetail.Response> result = await Sender.Send(new GetLeaveRequestDetail.Query(id), cancellationToken);

        return result.Match(
            onSuccess: value => Ok(value),
            onFailure: () => NotFound());
    }
}
