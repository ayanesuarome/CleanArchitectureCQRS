using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // POST api/<v>/leave-requests
    [HttpPost(ApiRoutes.LeaveRequests.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HasPermission(Domain.Enumerations.Permission.CreateLeaveRequest)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveRequestRequest request, CancellationToken cancellationToken)
    {
        CreateLeaveRequest.Command command = new(
            request.LeaveTypeId,
            request.StartDate,
            request.EndDate,
            request.Comments);

        Result<LeaveRequest> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        await Publisher.Publish(new LeaveRequestEvent(result.Value, LeaveRequestAction.Created));

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Value.Id },
            result.Value.Id);
    }
}
