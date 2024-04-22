using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET: api/<v>/<LeaveTypesController>
    [HttpGet(ApiRoutes.LeaveTypes.Get)]
    [ProducesResponseType(typeof(LeaveTypeListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        Result<LeaveTypeListDto> result = await _mediator.Send(new GetLeaveTypeList.GetLeaveTypeList.Query());
        return Ok(result.Value);
    }
}