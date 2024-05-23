using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // PUT api/<v>/leave-requests/5/cancel-request
    [HttpPut(ApiRoutes.LeaveRequests.CancelRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(LeaveRequestPermissions.CancelLeaveRequest)]
    public async Task<IActionResult> CancelRequest([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<LeaveRequest> result = await Sender.Send(new CancelLeaveRequest.Command(id), cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
