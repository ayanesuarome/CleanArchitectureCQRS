using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class LeaveAllocationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<v>/<LeaveAllocationController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetLeaveAllocationListQuery()));
    }

    // GET api/<v>/<LeaveAllocationController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveAllocationDetailsQuery(id)));
    }
}
