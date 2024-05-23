using CleanArch.Api.Contracts;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController
{
    // GET: api/admin/<v>/leave-requests
    [HttpGet(ApiRoutes.LeaveRequests.Get)]
    [ProducesResponseType(typeof(LeaveRequestListDto), StatusCodes.Status200OK)]
    [HasPermission(LeaveRequestPermissions.AccessLeaveRequests)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<LeaveRequestListDto> result = await Sender.Send(new AdminGetLeaveRequestList.AdminGetLeaveRequestList.Query(), cancellationToken);
        return Ok(result.Value);
    }
}
