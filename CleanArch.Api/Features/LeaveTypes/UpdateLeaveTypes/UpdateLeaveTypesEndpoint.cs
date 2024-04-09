using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // PUT api/<v>/<LeaveTypesController>/5
    [HttpPut(ApiRoutes.LeaveTypes.Put)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveTypeRequest request)
    {
        UpdateLeaveTypes.UpdateLeaveType.Command commnand = _mapper.Map<UpdateLeaveTypes.UpdateLeaveType.Command>(request);
        Result<Unit> result = await _mediator.Send(commnand);

        return result switch
        {
            SuccessResult<Unit> => NoContent(),
            NotFoundResult<Unit> => NotFound(),
            FailureResult<Unit> errorResult => BadRequest(errorResult.Error)
        };
    }
}
