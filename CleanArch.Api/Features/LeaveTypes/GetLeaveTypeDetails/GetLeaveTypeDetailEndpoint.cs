using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet(ApiRoutes.LeaveTypes.GetById)]
    [ProducesResponseType(typeof(LeaveTypeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Result<LeaveTypeDetailDto> result = await _mediator.Send(new GetLeaveTypeDetail.Query(Guid.NewGuid()));

        return result switch
        {
            SuccessResult<LeaveTypeDetailDto> success => Ok(success.Value),
            NotFoundResult<LeaveTypeDetailDto> => NotFound()
        };
    }
}
