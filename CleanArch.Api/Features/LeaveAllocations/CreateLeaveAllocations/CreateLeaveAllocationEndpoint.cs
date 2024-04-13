using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // POST api/<v>/<LeaveAllocationController>
    [HttpPost(ApiRoutes.LeaveAllocations.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationRequest request)
    {
        Result<int> rowsAffectedResult = await _mediator.Send(new CreateLeaveAllocation.Command(request.LeaveTypeId));

        if (rowsAffectedResult.IsFailure)
        {
            return BadRequest(rowsAffectedResult.Error);
        }

        return rowsAffectedResult.Data > 0 ?
            Created() :
            NoContent();
    }
}
