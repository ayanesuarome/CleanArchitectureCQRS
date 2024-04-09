using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // POST api/<v>/<LeaveRequestController>
    [HttpPost(ApiRoutes.LeaveRequests.Delete)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestRequest request)
    {
        CreateLeaveRequests.CreateLeaveRequest.Command command = _mapper.Map<CreateLeaveRequests.CreateLeaveRequest.Command>(request);
        Result<LeaveRequest> result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Created));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> successResult => CreatedAtAction(nameof(Get), new { successResult.Data.Id }),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult.Error)
        };
    }
}
