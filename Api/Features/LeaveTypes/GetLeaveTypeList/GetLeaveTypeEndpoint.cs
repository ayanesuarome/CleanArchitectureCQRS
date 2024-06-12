using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET: api/<v>/<LeaveTypesController>
    [HttpGet(ApiRoutes.LeaveTypes.Get)]
    [ProducesResponseType(typeof(GetLeaveTypeList.GetLeaveTypeList.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<GetLeaveTypeList.GetLeaveTypeList.Response> result = await Sender.Send(new GetLeaveTypeList.GetLeaveTypeList.Query(), cancellationToken);
        return Ok(result.Value);
    }
}