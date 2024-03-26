using CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using CleanArch.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotFoundResult = CleanArch.Application.Models.NotFoundResult;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

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

        return rowsAffectedResult.Data > 0 ? Created() : NoContent();
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
            NotFoundResult => NotFound(),
            ErrorResult errorResult => BadRequest(errorResult.Errors)
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
            NotFoundResult => NotFound(),
            ErrorResult errorResult => BadRequest(errorResult.Errors)
        };
    }
}
