using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET api/<v>/<LeaveAllocationController>/5
    [HttpGet(ApiRoutes.LeaveAllocations.GetById)]
    [ProducesResponseType(typeof(LeaveAllocationDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        Result<LeaveAllocationDetailsDto> result = await _mediator.Send(new GetLeaveAllocationDetail.Query(id));

        return result switch
        {
            SuccessResult<LeaveAllocationDetailsDto> successResult => Ok(successResult.Data),
            NotFoundResult<LeaveAllocationDetailsDto> => NotFound()
        };
    }
    
}
