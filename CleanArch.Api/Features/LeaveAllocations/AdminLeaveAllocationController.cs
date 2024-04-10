using AutoMapper;
using CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocation;
using CleanArch.Api.Infrastructure;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController(IMediator mediator, IMapper mapper) :
    BaseAdminController(mediator, mapper)
{
    // PUT api/admin/<v>/<LeaveAllocationController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationCommand leaveAllocation)
    {
        leaveAllocation.Id = id;
        Result result = await _mediator.Send(leaveAllocation);

        return result switch
        {
            SuccessResult => NoContent(),
            Application.ResultPattern.NotFoundResult => NotFound(),
            FailureResult errorResult => BadRequest(errorResult.Error)
        };
    }
}
