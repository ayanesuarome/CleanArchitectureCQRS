using Microsoft.AspNetCore.Mvc;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET: api/<v>/leave-requests
    [HttpGet(ApiRoutes.LeaveRequests.Get)]
    [ProducesResponseType(typeof(LeaveRequestListDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(CancellationToken cancellationToken)
    {
        Result<LeaveRequestListDto> result = await Sender.Send(
            new GetLeaveRequestList.GetLeaveRequestList.Query(),
            cancellationToken);

        return Ok(result.Value);
    }
}
