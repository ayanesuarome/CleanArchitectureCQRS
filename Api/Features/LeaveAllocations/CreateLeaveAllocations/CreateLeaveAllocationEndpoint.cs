using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // POST api/<v>/leave-allocations
    [HttpPost(ApiRoutes.LeaveAllocations.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HasPermission(LeaveAllocationPermissions.CreateLeaveAllocation)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveAllocation.Request request, CancellationToken cancellationToken)
    {
        Result<int> rowsAffectedResult = await Sender.Send(new CreateLeaveAllocation.Command(request.LeaveTypeId), cancellationToken);

        return rowsAffectedResult.Match(
            onSuccess: value => value > 0 ? Created() : NoContent(),
            onFailure: () => HandleFailure(rowsAffectedResult));
    }
}
