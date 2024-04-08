using CleanArch.Api.Contracts;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes
{
    public sealed partial class AdminLeaveTypeController
    {
        // DELETE api/<v>/<LeaveTypesController>/5
        [HttpDelete(ApiRoutes.LeaveTypes.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Result<Unit> result = await _mediator.Send(new DeleteLeaveTypes.DeleteLeaveType.Command(id));

            return result switch
            {
                SuccessResult<Unit> => NoContent(),
                NotFoundResult<Unit> notFoundResult => NotFound()
            };
        }
    }
}
