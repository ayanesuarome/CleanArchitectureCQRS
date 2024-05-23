using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // PUT api/<v>/leave-requests/5
    [HttpPut(ApiRoutes.LeaveRequests.Put)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(LeaveRequestPermissions.UpdateLeaveRequest)]
    public async Task<IActionResult> Put(
        [FromRoute] Guid id,
        [FromBody] UpdateLeaveRequestRequest request,
        CancellationToken cancellationToken)
    {
        UpdateLeaveRequest.Command command = new(
            id,
            request.StartDate,
            request.EndDate,
            request.Comments);

        Result<LeaveRequest> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
