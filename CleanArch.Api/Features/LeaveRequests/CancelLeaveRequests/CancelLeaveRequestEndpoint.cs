using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;
using CleanArch.Contracts;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // PUT api/<v>/leave-requests/5/cancel-request
    [HttpPut(ApiRoutes.LeaveRequests.CancelRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(Domain.Enumerations.Permission.CancelLeaveRequest)]
    public async Task<IActionResult> CancelRequest([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<LeaveRequest> result = await Sender.Send(new CancelLeaveRequest.Command(id), cancellationToken);
        await Publisher.Publish(new LeaveRequestEvent(result.Value, LeaveRequestAction.Canceled));

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult)
        };
    }
}
