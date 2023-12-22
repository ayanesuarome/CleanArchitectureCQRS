using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Application.Features.LeaveTypes.Commands.DeleteLeaveType;
using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveTypesController>
    [HttpGet]
    public async Task<List<LeaveTypeDto>> Get()
    {
        return await _mediator.Send(new GetLeaveTypeListQuery());
    }

    // GET api/<LeaveTypesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveTypeDetailsQuery(id)));
    }

    // POST api/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand leaveType)
    {
        int id = await _mediator.Send(leaveType);
        return CreatedAtAction(nameof(Get), id);
    }

    // PUT api/<LeaveTypesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveTypeCommand leaveType)
    {
        leaveType.Id = id;
        await _mediator.Send(leaveType);
        return NoContent();
    }

    // DELETE api/<LeaveTypesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveTypeCommand(id));
        return NoContent();
    }
}
