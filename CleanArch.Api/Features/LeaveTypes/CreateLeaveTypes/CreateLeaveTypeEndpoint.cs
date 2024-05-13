using Microsoft.AspNetCore.Mvc;
using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Identity.Authentication;
using CleanArch.Domain.Enumerations;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // POST api/<v>/<LeaveTypesController>
    [HttpPost(ApiRoutes.LeaveTypes.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    [HasPermission(Permission.CreateLeaveType)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveTypeRequest request, CancellationToken cancellationToken)
    {
        CreateLeaveType.Command commnad = new(request.Name, request.DefaultDays);
        Result<Guid> result = await Sender.Send(commnad, cancellationToken);

        return result switch
        {
            SuccessResult<Guid> successResult => CreatedAtAction(nameof(Get), new { successResult.Value }),
            FailureResult<Guid> errorResult => BadRequest(errorResult)
        };
    }
}
