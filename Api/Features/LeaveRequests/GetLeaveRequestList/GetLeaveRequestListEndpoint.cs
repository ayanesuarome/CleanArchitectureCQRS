using Microsoft.AspNetCore.Mvc;
using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET: api/<v>/leave-requests
    [HttpGet(ApiRoutes.LeaveRequests.Get)]
    [ProducesResponseType(typeof(GetLeaveRequestList.GetLeaveRequestList.Response), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(CancellationToken cancellationToken)
    {
        Result<GetLeaveRequestList.GetLeaveRequestList.Response> result = await Sender.Send(
            new GetLeaveRequestList.GetLeaveRequestList.Query(),
            cancellationToken);

        return Ok(result.Value);
    }
}
