using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // POST api/<v>/leave-requests
    [HttpPost(ApiRoutes.LeaveRequests.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HasPermission(LeaveRequestPermissions.CreateLeaveRequest)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveRequest.CreateRequest request, CancellationToken cancellationToken)
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

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Value.Id },
            result.Value.Id);
    }
}
