using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Enumerations;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
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
    [HasPermission(Permission.UpdateLeaveType)]
    public async Task<IActionResult> Put(
        [FromRoute] Guid id,
        [FromBody] UpdateLeaveTypeRequest request,
        CancellationToken cancellationToken)
    {
        UpdateLeaveType.Command command = new(id, request.Name, request.DefaultDays);
        Result<Unit> result = await Sender.Send(command, cancellationToken);

        return result switch
        {
            SuccessResult<Unit> => NoContent(),
            NotFoundResult<Unit> => NotFound(),
            FailureResult<Unit> errorResult => BadRequest(errorResult.Error)
        };
    }
}
