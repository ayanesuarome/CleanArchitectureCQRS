using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Contracts;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET: api/<v>/<LeaveRequestController>
    [HttpGet(ApiRoutes.LeaveRequests.Get)]
    [ProducesResponseType(typeof(LeaveRequestListDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        Result<LeaveRequestListDto> result = await _mediator.Send(new GetLeaveRequestList.GetLeaveRequestList.Query());
        return Ok(result.Data);
    }
}
