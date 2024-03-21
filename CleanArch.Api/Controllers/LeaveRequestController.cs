using CleanArch.Application.Events;
using CleanArch.Application.Extensions;
using CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class LeaveRequestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<v>/<LeaveRequestController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetLeaveRequestListQuery()));
    }

    // GET api/<v>/<LeaveRequestController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveRequestDetailsQuery(id)));
    }

    // POST api/<v>/<LeaveRequestController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand model)
    {
        LeaveRequest leaveRequest = await _mediator.Send(model);
        await _mediator.Publish(new LeaveRequestEvent(leaveRequest, LeaveRequestAction.Created));

        return CreatedAtAction(nameof(Get), new { leaveRequest.Id });
    }

    // PUT api/<v>/<LeaveRequestController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestCommand model)
    {
        model.Id = id;
        Result<LeaveRequest> result = await _mediator.Send(model);
        await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Updated));

        return NoContent();
    }

    // DELETE api/<v>/<LeaveRequestController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveRequestCommand(id));
        return NoContent();
    }

    // PUT api/<v>/<LeaveRequestController>/5/CancelRequest
    [HttpPut("{id}/CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(int id)
    {
        Result<LeaveRequest> result = await _mediator.Send(new CancelLeaveRequestCommand(id));
        await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Canceled));

        return result switch
        {
            SuccessResult<LeaveRequest> successResult => NoContent(),
            NotFoundResult<LeaveRequest> notFoundResult => NotFound(),
            ErrorResult<LeaveRequest> errorResult => BadRequest(errorResult.Errors)
        };
    }
}
