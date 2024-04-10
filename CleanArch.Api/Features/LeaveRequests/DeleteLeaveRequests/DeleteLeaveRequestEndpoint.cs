using AutoMapper;
using CleanArch.Api.Contracts;
using CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;
using CleanArch.Application.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController
{
    // DELETE api/<v>/<LeaveRequestController>/5
    [HttpDelete(ApiRoutes.LeaveRequests.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        Result result = await _mediator.Send(new DeleteLeaveRequest.Command(id));

        return result switch
        {
            SuccessResult => NoContent(),
            Application.ResultPattern.NotFoundResult => NotFound()
        };
    }
}
