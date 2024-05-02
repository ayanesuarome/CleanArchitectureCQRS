using CleanArch.Contracts;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET: api/<v>/leave-allocations
    [HttpGet(ApiRoutes.LeaveAllocations.Get)]
    [ProducesResponseType(typeof(LeaveAllocationListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        Result<LeaveAllocationListDto> result = await _sender.Send(new GetLeaveAllocationList.GetLeaveAllocationList.Query());
        return Ok(result.Value);
    }
}
