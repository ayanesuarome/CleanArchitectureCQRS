using CleanArch.Application.Features.LeaveTypes.Commands.DeleteLeaveType;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers
{
    [Route("api/admin/v{version:apiVersion}/[controller]")]
    public class AdminLeaveTypeController(IMediator mediator) : BaseAdminController
    {
        private readonly IMediator _mediator = mediator;

        // DELETE api/<v>/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Result<Unit> result = await _mediator.Send(new DeleteLeaveTypeCommand(id));
            
            return result switch
            {
                SuccessResult<Unit> => NoContent(),
                NotFoundResult<Unit> notFoundResult => NotFound()
            };
        }
    }
}
