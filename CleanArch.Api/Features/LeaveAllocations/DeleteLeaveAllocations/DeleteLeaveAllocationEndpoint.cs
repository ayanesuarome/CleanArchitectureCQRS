﻿using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;
using CleanArch.Contracts;
using CleanArch.Domain.Enumerations;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // DELETE api/admin/<v>/leave-allocations/5
    [HttpDelete(ApiRoutes.LeaveAllocations.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(Permission.DeleteLeaveAllocation)]
    public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(new DeleteLeaveAllocation.Command(id), cancellationToken);

        return result switch
        {
            SuccessResult => NoContent(),
            Domain.Primitives.Result.NotFoundResult => NotFound(),
            FailureResult errorResult => BadRequest(errorResult)
        };
    }
}
