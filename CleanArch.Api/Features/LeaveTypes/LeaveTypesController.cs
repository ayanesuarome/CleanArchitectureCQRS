using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Application.ResultPattern;
using AutoMapper;
using CleanArch.Api.Infrastructure;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController(IMediator mediator, IMapper mapper)
    : BaseApiController(mediator, mapper)
{
    // GET: api/<v>/<LeaveTypesController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        Result<List<LeaveTypeDto>> result = await _mediator.Send(new GetLeaveTypeListQuery());
        return Ok(result.Data);
    }

    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        Result<LeaveTypeDetailsDto> result = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));

        return result switch
        {
            SuccessResult<LeaveTypeDetailsDto> success => Ok(success.Data),
            NotFoundResult<LeaveTypeDetailsDto> => NotFound()
        };
    }
}
