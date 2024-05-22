using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // PUT api/<v>/<LeaveTypesController>/5
    [HttpPut(ApiRoutes.LeaveTypes.Put)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HasPermission(Permissions.UpdateLeaveType)]
    public async Task<IActionResult> Put(
        [FromRoute] Guid id,
        [FromBody] UpdateLeaveTypeRequest request,
        CancellationToken cancellationToken)
    {
        UpdateLeaveType.Command command = new(id, request.Name, request.DefaultDays);
        Result<Unit> result = await Sender.Send(command, cancellationToken);

        return result.Match(
            onSuccess: () => NoContent(),
            onFailure: () => HandleFailure(result));
    }
}
