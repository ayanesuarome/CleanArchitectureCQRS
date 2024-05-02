using CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // POST api/<v>/leave-allocations
    [HttpPost(ApiRoutes.LeaveAllocations.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationRequest request)
    {
        Result<int> rowsAffectedResult = await _sender.Send(new CreateLeaveAllocation.Command(request.LeaveTypeId));

        if (rowsAffectedResult.IsFailure)
        {
            return BadRequest(rowsAffectedResult.Error);
        }

        return rowsAffectedResult.Value > 0 ?
            Created() :
            NoContent();
    }
}
