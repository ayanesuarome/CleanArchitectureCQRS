﻿using CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Enumerations;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET api/<v>/leave-allocations/5
    [HttpGet(ApiRoutes.LeaveAllocations.GetById)]
    [ProducesResponseType(typeof(LeaveAllocationDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(Permission.AccessLeaveAllocations)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<LeaveAllocationDetailsDto> result = await Sender.Send(new GetLeaveAllocationDetail.Query(id), cancellationToken);

        return result switch
        {
            SuccessResult<LeaveAllocationDetailsDto> successResult => Ok(successResult.Value),
            NotFoundResult<LeaveAllocationDetailsDto> => NotFound()
        };
    }
    
}
