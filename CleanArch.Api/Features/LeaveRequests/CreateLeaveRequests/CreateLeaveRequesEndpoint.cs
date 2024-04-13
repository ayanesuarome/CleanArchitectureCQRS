﻿using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // POST api/<v>/<LeaveRequestController>
    [HttpPost(ApiRoutes.LeaveRequests.Delete)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveRequestRequest request)
    {
        CreateLeaveRequest.Command command = _mapper.Map<CreateLeaveRequest.Command>(request);
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
