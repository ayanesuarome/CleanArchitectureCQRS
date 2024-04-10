using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController
{
    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet(ApiRoutes.LeaveTypes.GetById)]
    [ProducesResponseType(typeof(LeaveTypeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        Result<LeaveTypeDetailDto> result = await _mediator.Send(new GetLeaveTypeDetail.Query(id));

        return result switch
        {
            SuccessResult<LeaveTypeDetailDto> success => Ok(success.Data),
            NotFoundResult<LeaveTypeDetailDto> => NotFound()
        };
    }
}
