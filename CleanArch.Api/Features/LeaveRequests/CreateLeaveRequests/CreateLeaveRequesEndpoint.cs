﻿using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using CleanArch.Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // POST api/<v>/leave-requests
    [HttpPost(ApiRoutes.LeaveRequests.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveRequestRequest request, CancellationToken cancellationToken)
    {
        CreateLeaveRequest.Command command = new(
            request.LeaveTypeId,
            request.StartDate,
            request.EndDate,
            request.Comments);

        Result<LeaveRequest> result = await Sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            await Publisher.Publish(new LeaveRequestEvent(result.Value, LeaveRequestAction.Created));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> successResult => CreatedAtAction(nameof(Get), new { successResult.Value.Id }),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult)
        };
    }
}
