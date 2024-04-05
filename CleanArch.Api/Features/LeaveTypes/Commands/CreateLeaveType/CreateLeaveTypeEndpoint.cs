using Microsoft.AspNetCore.Mvc;
using CleanArch.Application.ResultPattern;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // POST api/<v>/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveType.Command command)
    {
        Result<int> result = await _mediator.Send(command);

        return result switch
        {
            SuccessResult<int> successResult => CreatedAtAction(nameof(Get), new { successResult.Data }),
            FailureResult<int> errorResult => BadRequest(errorResult.Error)
        };
    }
}
