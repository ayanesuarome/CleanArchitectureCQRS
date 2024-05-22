using Microsoft.AspNetCore.Mvc;
using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Identity.Authentication;
using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Authentication;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // POST api/<v>/<LeaveTypesController>
    [HttpPost(ApiRoutes.LeaveTypes.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HasPermission(Permissions.CreateLeaveType)]
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
