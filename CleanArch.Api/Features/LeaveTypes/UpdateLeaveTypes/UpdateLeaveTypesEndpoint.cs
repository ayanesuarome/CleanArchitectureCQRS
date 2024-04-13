using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Domain.Primitives.Result;
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
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLeaveTypeRequest request)
    {
        UpdateLeaveType.Command command = _mapper.Map<UpdateLeaveType.Command>(request);
        command.Id = id;

        Result<Unit> result = await _mediator.Send(command);

        return result switch
        {
            SuccessResult<Unit> => NoContent(),
            NotFoundResult<Unit> => NotFound(),
            FailureResult<Unit> errorResult => BadRequest(errorResult.Error)
        };
    }
}
