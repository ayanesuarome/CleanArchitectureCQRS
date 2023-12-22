using CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaLeaveRequestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<LeaveRequestController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetLeaveRequestListQuery()));
    }

    // GET api/<LeaveRequestController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveRequestDetailsQuery(id)));
    }

    // POST api/<LeaveRequestController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand model)
    {
        int id = await _mediator.Send(model);
        return CreatedAtAction(nameof(Get), id);
    }

    // PUT api/<LeaveRequestController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestCommand model)
    {
        model.Id = id;
        await _mediator.Send(model);
        return NoContent();
    }

    // DELETE api/<LeaveRequestController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveRequestCommand(id));
        return NoContent();
    }

    // PUT api/<LeaveRequestController>/5/CancelRequest
    [HttpPut("{id}/CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(int id)
    {
        await _mediator.Send(new CancelLeaveRequestCommand(id));
        return NoContent();
    }

    // PUT api/<LeaveRequestController>/5/UpdateApproval
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
