using CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;
using CleanArch.Contracts;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // PUT api/<v>/leave-requests/5/cancel-request
    [HttpPut(ApiRoutes.LeaveRequests.CancelRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelRequest([FromRoute] int id)
    {
        Result<LeaveRequest> result = await _mediator.Send(new CancelLeaveRequest.Command(id));
        await _mediator.Publish(new LeaveRequestEvent(result.Value, LeaveRequestAction.Canceled));

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult)
        };
    }
}
