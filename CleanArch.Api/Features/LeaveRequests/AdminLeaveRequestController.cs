using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Infrastructure;
using CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController(IMediator mediator, IMapper mapper)
    : BaseAdminController(mediator, mapper)
{
    // GET: api/admin/<v>/<AdminLeaveRequestController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        Result<List<LeaveRequestDto>> result = await _mediator.Send(new AdminGetLeaveRequestListQuery());
        return Ok(result.Data);
    }
}
