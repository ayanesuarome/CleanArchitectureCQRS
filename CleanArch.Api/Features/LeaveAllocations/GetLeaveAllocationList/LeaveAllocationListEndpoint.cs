using CleanArch.Contracts;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Enumerations;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET: api/<v>/leave-allocations
    [HttpGet(ApiRoutes.LeaveAllocations.Get)]
    [ProducesResponseType(typeof(LeaveAllocationListDto), StatusCodes.Status200OK)]
    [HasPermission(Permission.AccessLeaveAllocations)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<LeaveAllocationListDto> result = await Sender.Send(new GetLeaveAllocationList.GetLeaveAllocationList.Query(), cancellationToken);
        return Ok(result.Value);
    }
}
