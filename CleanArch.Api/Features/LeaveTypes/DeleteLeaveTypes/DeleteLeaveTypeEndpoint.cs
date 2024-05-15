using CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Contracts;
using CleanArch.Identity.Authentication;
using CleanArch.Domain.Enumerations;
using CleanArch.Api.Contracts;

namespace CleanArch.Api.Features.LeaveTypes
{
    public sealed partial class AdminLeaveTypeController
    {
        // DELETE api/<v>/<LeaveTypesController>/5
        [HttpDelete(ApiRoutes.LeaveTypes.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HasPermission(Permission.DeleteLeaveType)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            Result<Unit> result = await Sender.Send(new DeleteLeaveType.Command(id), cancellationToken);

            return result switch
            {
                SuccessResult<Unit> => NoContent(),
                NotFoundResult<Unit> notFoundResult => NotFound()
            };
        }
    }
}
