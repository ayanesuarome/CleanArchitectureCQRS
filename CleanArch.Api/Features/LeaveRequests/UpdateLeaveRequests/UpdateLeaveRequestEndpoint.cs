using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // PUT api/<v>/<LeaveRequestController>/5
    [HttpPut(ApiRoutes.LeaveRequests.Put)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLeaveRequestRequest request)
    {
        UpdateLeaveRequest.Command command = _mapper.Map<UpdateLeaveRequest.Command>(request);
        Result<LeaveRequest> result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Updated));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult.Error)
        };
    }
}
