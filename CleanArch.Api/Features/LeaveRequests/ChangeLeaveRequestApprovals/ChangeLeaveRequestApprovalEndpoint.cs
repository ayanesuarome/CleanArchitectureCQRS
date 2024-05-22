using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveRequests.Events;
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
    [HasPermission(Permissions.ChangeLeaveRequestApproval)]
    public async Task<IActionResult> UpdateApproval(
        [FromRoute] Guid id,
        [FromBody] ChangeLeaveRequestApprovalRequest request,
        CancellationToken cancellationToken)
    {
        ChangeLeaveRequestApproval.Command command = new(id, request.Approved);
        Result<LeaveRequest> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        await Publisher.Publish(new LeaveRequestEvent(result.Value, LeaveRequestAction.UpdateApproval));

        return NoContent();
    }
}
