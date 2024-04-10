using AutoMapper;
using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Infrastructure;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController(IMediator mediator, IMapper mapper)
    : BaseApiController(mediator, mapper)
{
    // GET: api/<v>/<LeaveRequestController>
    [HttpGet(ApiRoutes.LeaveRequests.)]
    [ProducesResponseType(typeof(LeaveRequestListDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        Result<List<LeaveRequestDto>> result = await _mediator.Send(new GetLeaveRequestListQuery());
        return Ok(result.Data);
    }
}
