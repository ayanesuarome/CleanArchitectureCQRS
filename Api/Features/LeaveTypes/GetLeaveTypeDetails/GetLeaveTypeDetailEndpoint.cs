using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet(ApiRoutes.LeaveTypes.GetById)]
    [ProducesResponseType(typeof(GetLeaveTypeDetail.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        Result<GetLeaveTypeDetail.Response> result = await Sender.Send(new GetLeaveTypeDetail.Query(id), cancellationToken);

        return result.Match(
            onSuccess: value => Ok(value),
            onFailure: () => NotFound());
    }
}
