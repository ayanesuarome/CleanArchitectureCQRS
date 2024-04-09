using CleanArch.Api.Infrastructure;
using CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Features.LeaveAllocations;

[Route("api/admin/v{version:apiVersion}/[controller]")]
public class AdminLeaveAllocationController(IMediator mediator) : BaseAdminController
{
    private readonly IMediator _mediator = mediator;

    // POST api/<v>/<LeaveAllocationController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand leaveAllocation)
    {
        Result<int> rowsAffectedResult = await _mediator.Send(leaveAllocation);

        if (rowsAffectedResult.IsFailure)
        {
            return BadRequest(rowsAffectedResult.Error);
        }

        return rowsAffectedResult.Data > 0 ?
            Created() :
            NoContent();
    }

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

    // DELETE api/admin/<v>/<LeaveAllocationController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        Result result = await _mediator.Send(new DeleteLeaveAllocationCommand(id));

        return result switch
        {
            SuccessResult => NoContent(),
            Application.ResultPattern.NotFoundResult => NotFound(),
            FailureResult errorResult => BadRequest(errorResult.Error)
        };
    }
}
