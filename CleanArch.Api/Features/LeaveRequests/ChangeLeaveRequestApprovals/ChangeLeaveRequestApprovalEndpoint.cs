using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using CleanArch.Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController
{
    // PUT api/admin/<v>/leave-requests/5/update-approval
    [HttpPut(ApiRoutes.LeaveRequests.AdminUpdateApproval)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateApproval(
        [FromRoute] Guid id,
        [FromBody] ChangeLeaveRequestApprovalRequest request,
        CancellationToken cancellationToken)
    {
        ChangeLeaveRequestApproval.Command command = new(id, request.Approved);
        Result<LeaveRequest> result = await Sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            await Publisher.Publish(new LeaveRequestEvent(result.Value, LeaveRequestAction.UpdateApproval));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult.Error)
        };
    }
}
