using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController
{
    // PUT api/admin/<v>/leave-requests/5/update-approval
    [HttpPut(ApiRoutes.LeaveRequests.AdminUpdateApproval)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(LeaveRequestPermissions.ChangeLeaveRequestApproval)]
    public async Task<IActionResult> UpdateApproval(
        [FromRoute] Guid id,
        [FromBody] ChangeLeaveRequestApproval.Request request,
        CancellationToken cancellationToken)
    {
        ChangeLeaveRequestApproval.Command command = new(id, request.Approved);
        Result<LeaveRequest> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
