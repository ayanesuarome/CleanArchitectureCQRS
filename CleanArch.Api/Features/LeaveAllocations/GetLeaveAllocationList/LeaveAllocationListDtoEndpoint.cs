using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET: api/<v>/<LeaveAllocationController>
    [HttpGet(ApiRoutes.LeaveAllocations.Get)]
    [ProducesResponseType(typeof(LeaveAllocationListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        Result<LeaveAllocationListDto> result = await _mediator.Send(new GetLeaveAllocationList.GetLeaveAllocationList.Query());
        return Ok(result.Data);
    }
}
