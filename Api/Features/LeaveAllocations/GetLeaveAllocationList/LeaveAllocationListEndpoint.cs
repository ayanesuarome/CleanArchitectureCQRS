using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController
{
    // GET: api/<v>/leave-allocations
    [HttpGet(ApiRoutes.LeaveAllocations.Get)]
    [ProducesResponseType(typeof(GetLeaveAllocationList.GetLeaveAllocationList.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<GetLeaveAllocationList.GetLeaveAllocationList.Response> result = await Sender.Send(
            new GetLeaveAllocationList.GetLeaveAllocationList.Query(),
            cancellationToken);

        return Ok(result.Value);
    }
}
