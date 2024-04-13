using Microsoft.AspNetCore.Mvc;
using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // POST api/<v>/<LeaveTypesController>
    [HttpPost(ApiRoutes.LeaveTypes.Post)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateLeaveTypeRequest request)
    {
        CreateLeaveType.Command commnad = _mapper.Map<CreateLeaveType.Command>(request);
        Result<int> result = await _mediator.Send(commnad);

        return result switch
        {
            SuccessResult<int> successResult => CreatedAtAction(nameof(Get), new { successResult.Data }),
            FailureResult<int> errorResult => BadRequest(errorResult.Error)
        };
    }
}
