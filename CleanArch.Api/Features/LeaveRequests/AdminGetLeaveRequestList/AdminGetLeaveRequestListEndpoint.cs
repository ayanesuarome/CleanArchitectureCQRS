using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController
{
    // GET: api/admin/<v>/<AdminLeaveRequestController>
    [HttpGet(ApiRoutes.LeaveRequests.AdminGet)]
    [ProducesResponseType(typeof(LeaveRequestListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        Result<LeaveRequestListDto> result = await _mediator.Send(new AdminGetLeaveRequestList.AdminGetLeaveRequestList.Query());
        return Ok(result.Data);
    }
}
