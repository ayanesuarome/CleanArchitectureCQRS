using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;
using CleanArch.Application.ResultPattern;
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
            Application.ResultPattern.NotFoundResult => NotFound(),
            FailureResult errorResult => BadRequest(errorResult.Error)
        };
    }
}
