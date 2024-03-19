using CleanArch.Application.Events;
using CleanArch.Application.Extensions;
using CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        Result<List<LeaveRequestDto>> result = await _mediator.Send(new AdminGetLeaveRequestListQuery());
        return Ok(result.Data);
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
        Result<LeaveRequest> result = await _mediator.Send(model);
        
        if(result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.UpdateApproval));
        }

        return result.Match<ActionResult, LeaveRequest>(
            onSuccess: () => NoContent(),
            onFailure: error => BadRequest(error));
    }
}
