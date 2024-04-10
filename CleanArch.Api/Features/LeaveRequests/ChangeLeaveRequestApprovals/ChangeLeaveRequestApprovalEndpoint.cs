using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController
{
    // PUT api/admin/<v>/<AdminLeaveRequestController>/5/UpdateApproval
    [HttpPut("{id}/UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(int id, [FromBody] ChangeLeaveRequestApprovalRequest request)
    {
        ChangeLeaveRequestApproval.Command command = _mapper.Map<ChangeLeaveRequestApproval.Command>(request);
        command.Id = id;
        
        Result<LeaveRequest> result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.UpdateApproval));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult.Error)
        };
    }
}
