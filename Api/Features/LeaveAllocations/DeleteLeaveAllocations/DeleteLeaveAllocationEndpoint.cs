using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController
{
    // DELETE api/admin/<v>/leave-allocations/5
    [HttpDelete(ApiRoutes.LeaveAllocations.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(LeaveAllocationPermissions.DeleteLeaveAllocation)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(new DeleteLeaveAllocation.Command(id), cancellationToken);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: () => NotFound());
    }
}
