using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;
using CleanArch.Contracts;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // DELETE api/<v>/leave-requests/5
    [HttpDelete(ApiRoutes.LeaveRequests.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(Domain.Enumerations.Permission.DeleteLeaveRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(new DeleteLeaveRequest.Command(id), cancellationToken);

        return result switch
        {
            SuccessResult => NoContent(),
            Domain.Primitives.Result.NotFoundResult => NotFound()
        };
    }
}
