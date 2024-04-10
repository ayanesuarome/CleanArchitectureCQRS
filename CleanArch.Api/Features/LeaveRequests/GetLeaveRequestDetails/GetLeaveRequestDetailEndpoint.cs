using CleanArch.Api.Contracts;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;
using CleanArch.Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET api/<v>/<LeaveRequestController>/5
    [HttpGet(ApiRoutes.LeaveRequests.GetById)]
    [ProducesResponseType(typeof(LeaveRequestDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        Result<LeaveRequestDetailsDto> result = await _mediator.Send(new GetLeaveRequestDetail.Query(id));

        return result switch
        {
            SuccessResult<LeaveRequestDetailsDto> success => Ok(success.Data),
            NotFoundResult<LeaveRequestDetailsDto> => NotFound()
        };
    }
}
