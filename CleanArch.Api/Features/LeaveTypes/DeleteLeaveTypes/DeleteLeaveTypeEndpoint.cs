using CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Identity.Authentication;
using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Authentication;

namespace CleanArch.Api.Features.LeaveTypes
{
    public sealed partial class AdminLeaveTypeController
    {
        // DELETE api/admin/<v>/leave-types>/5
        [HttpDelete(ApiRoutes.LeaveTypes.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HasPermission(Permissions.DeleteLeaveType)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            Result<Unit> result = await Sender.Send(new DeleteLeaveType.Command(id), cancellationToken);

            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: () => NotFound());
        }
    }
}
