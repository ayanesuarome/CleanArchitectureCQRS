using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController
{
    // GET: api/admin/<v>/leave-requests
    [HttpGet(ApiRoutes.LeaveRequests.Get)]
    [ProducesResponseType(typeof(AdminGetLeaveRequestList.AdminGetLeaveRequestList.Response), StatusCodes.Status200OK)]
    [HasPermission(LeaveRequestPermissions.AccessLeaveRequests)]
    public async Task<IActionResult> Get(
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        Result<AdminGetLeaveRequestList.AdminGetLeaveRequestList.Response> result =
            await Sender.Send(new AdminGetLeaveRequestList.AdminGetLeaveRequestList.Query(
                searchTerm,
                sortColumn,
                sortOrder,
                page,
                pageSize),
            cancellationToken);

        return Ok(result.Value);
    }
}
