using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequest;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequest;
using CleanArch.Api.Infrastructure;
using CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController(IMediator mediator, IMapper mapper)
    : BaseApiController(mediator, mapper)
{
    // GET: api/<v>/<LeaveRequestController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        Result<List<LeaveRequestDto>> result = await _mediator.Send(new GetLeaveRequestListQuery());
        return Ok(result.Data);
    }

    // GET api/<v>/<LeaveRequestController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        Result<LeaveRequestDetailsDto> result = await _mediator.Send(new GetLeaveRequestDetailsQuery(id));

        return result switch
        {
            SuccessResult<LeaveRequestDetailsDto> success => Ok(success.Data),
            NotFoundResult<LeaveRequestDetailsDto> => NotFound()
        };
    }
}
