using CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // GET api/<v>/leave-requests/5
    [HttpGet(ApiRoutes.LeaveRequests.GetById)]
    [ProducesResponseType(typeof(LeaveRequestDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Result<LeaveRequestDetailsDto> result = await _mediator.Send(new GetLeaveRequestDetail.Query(id));

        return result switch
        {
            SuccessResult<LeaveRequestDetailsDto> success => Ok(success.Value),
            NotFoundResult<LeaveRequestDetailsDto> => NotFound()
        };
    }
}
