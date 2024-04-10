using AutoMapper;
using CleanArch.Api.Infrastructure;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController(IMediator mediator, IMapper mapper)
    : BaseController(mediator, mapper)
{
    // GET: api/<v>/<LeaveAllocationController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        Result<List<LeaveAllocationDto>> result = await _mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(result.Data);
    }

    // GET api/<v>/<LeaveAllocationController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        Result<LeaveAllocationDetailsDto> result = await _mediator.Send(new GetLeaveAllocationDetailsQuery(id));
        
        return result switch
        {
            SuccessResult<LeaveAllocationDetailsDto> successResult => Ok(successResult.Data),
            NotFoundResult<LeaveAllocationDetailsDto> => NotFound()
        };
    }
}
