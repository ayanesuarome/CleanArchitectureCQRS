using Microsoft.AspNetCore.Mvc;
using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Identity.Authentication;
using CleanArch.Domain.Enumerations;
using CleanArch.Api.Contracts;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // POST api/<v>/<LeaveTypesController>
    [HttpPost(ApiRoutes.LeaveTypes.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HasPermission(Permission.CreateLeaveType)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveTypeRequest request, CancellationToken cancellationToken)
    {
        CreateLeaveType.Command commnad = new(request.Name, request.DefaultDays);
        Result<Guid> result = await Sender.Send(commnad, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Value },
            result.Value);
    }
}
