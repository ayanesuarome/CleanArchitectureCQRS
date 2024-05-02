﻿using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET: api/<v>/leave-requests
    [HttpGet(ApiRoutes.LeaveRequests.Get)]
    [ProducesResponseType(typeof(LeaveRequestListDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        Result<LeaveRequestListDto> result = await _sender.Send(new GetLeaveRequestList.GetLeaveRequestList.Query());
        return Ok(result.Value);
    }
}
