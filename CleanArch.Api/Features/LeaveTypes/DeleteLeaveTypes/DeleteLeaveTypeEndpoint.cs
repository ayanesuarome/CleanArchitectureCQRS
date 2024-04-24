using CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Contracts;

namespace CleanArch.Api.Features.LeaveTypes
{
    public sealed partial class AdminLeaveTypeController
    {
        // DELETE api/<v>/<LeaveTypesController>/5
        [HttpDelete(ApiRoutes.LeaveTypes.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<Unit> result = await _mediator.Send(new DeleteLeaveType.Command(id));

            return result switch
            {
                SuccessResult<Unit> => NoContent(),
                NotFoundResult<Unit> notFoundResult => NotFound()
            };
        }
    }
}
