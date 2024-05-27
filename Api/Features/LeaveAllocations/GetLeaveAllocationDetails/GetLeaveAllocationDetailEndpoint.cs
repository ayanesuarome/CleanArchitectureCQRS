﻿using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET api/<v>/leave-allocations/5
    [HttpGet(ApiRoutes.LeaveAllocations.GetById)]
    [ProducesResponseType(typeof(LeaveAllocationDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<LeaveAllocationDetailsDto> result = await Sender.Send(new GetLeaveAllocationDetail.Query(id), cancellationToken);

        return result.Match(
            onSuccess: value => Ok(value),
            onFailure: () => NotFound());
    }
    
}