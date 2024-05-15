using CleanArch.Api.Contracts;
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
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<LeaveAllocationListDto> result = await Sender.Send(new GetLeaveAllocationList.GetLeaveAllocationList.Query(), cancellationToken);
        return Ok(result.Value);
    }
}
