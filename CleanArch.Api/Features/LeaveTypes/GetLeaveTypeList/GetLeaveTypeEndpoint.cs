using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Enumerations;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Identity.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET: api/<v>/<LeaveTypesController>
    [HttpGet(ApiRoutes.LeaveTypes.Get)]
    [ProducesResponseType(typeof(LeaveTypeListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<LeaveTypeListDto> result = await Sender.Send(new GetLeaveTypeList.GetLeaveTypeList.Query(), cancellationToken);
        return Ok(result.Value);
    }
}