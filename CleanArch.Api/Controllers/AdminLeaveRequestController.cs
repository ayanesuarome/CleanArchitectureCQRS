using CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/admin/v{version:apiVersion}/[controller]")]
public class AdminLeaveRequestController(IMediator mediator) : BaseAdminController
{
    private readonly IMediator _mediator = mediator;

    // GET: api/admin/<v>/<AdminLeaveRequestController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        return Ok(await _mediator.Send(new AdminGetLeaveRequestListQuery()));
    }

    // PUT api/admin/<v>/<AdminLeaveRequestController>/5/UpdateApproval
    [HttpPut("{id}/UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(int id, [FromBody] ChangeLeaveRequestApprovalCommand model)
    {
        model.Id = id;
        await _mediator.Send(model);
        return NoContent();
    }
}
