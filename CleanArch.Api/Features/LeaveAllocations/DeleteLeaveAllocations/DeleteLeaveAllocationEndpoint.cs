using CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;
using CleanArch.Contracts;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // DELETE api/admin/<v>/leave-allocations/5
    [HttpDelete(ApiRoutes.LeaveAllocations.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        Result result = await _mediator.Send(new DeleteLeaveAllocation.Command(id));

        return result switch
        {
            SuccessResult => NoContent(),
            Domain.Primitives.Result.NotFoundResult => NotFound(),
            FailureResult errorResult => BadRequest(errorResult.Error)
        };
    }
}
