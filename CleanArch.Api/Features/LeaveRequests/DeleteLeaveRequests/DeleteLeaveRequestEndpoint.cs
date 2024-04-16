﻿using CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;
using CleanArch.Contracts;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // DELETE api/<v>/leave-requests/5
    [HttpDelete(ApiRoutes.LeaveRequests.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        Result result = await _mediator.Send(new DeleteLeaveRequest.Command(id));

        return result switch
        {
            SuccessResult => NoContent(),
            Domain.Primitives.Result.NotFoundResult => NotFound()
        };
    }
}
