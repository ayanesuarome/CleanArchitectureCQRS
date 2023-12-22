using CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<LeaveAllocationController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetLeaveAllocationListQuery()));
    }

    // GET api/<LeaveAllocationController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveAllocationDetailsQuery(id)));
    }

    // POST api/<LeaveAllocationController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand leaveAllocation)
    {
        int id = await _mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), id);
    }

    // PUT api/<LeaveAllocationController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationCommand leaveAllocation)
    {
        leaveAllocation.Id = id;
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand(id));
        return NoContent();
    }
}
