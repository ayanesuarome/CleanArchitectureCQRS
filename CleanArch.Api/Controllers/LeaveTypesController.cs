using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArch.Application.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class LeaveTypesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<v>/<LeaveTypesController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetLeaveTypeListQuery()));
    }

    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveTypeDetailsQuery(id)));
    }

    // POST api/<v>/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand leaveType)
    {
        Result<int> result = await _mediator.Send(leaveType);
        return CreatedAtAction(nameof(Get), result.Data);
    }

    // PUT api/<v>/<LeaveTypesController>/5
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
}
