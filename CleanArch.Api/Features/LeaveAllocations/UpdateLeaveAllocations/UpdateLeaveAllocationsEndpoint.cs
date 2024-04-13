using CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // PUT api/admin/<v>/<LeaveAllocationController>/5
    [HttpPut(ApiRoutes.LeaveAllocations.Put)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationRequest request)
    {
        UpdateLeaveAllocation.Command command = _mapper.Map<UpdateLeaveAllocation.Command>(request);
        Result result = await _mediator.Send(command);

        return result switch
        {
            SuccessResult => NoContent(),
            Domain.Primitives.Result.NotFoundResult => NotFound(),
            FailureResult errorResult => BadRequest(errorResult.Error)
        };
    }
}
