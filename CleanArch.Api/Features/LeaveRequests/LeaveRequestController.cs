using AutoMapper;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequest;
using CleanArch.Api.Infrastructure;
using CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController(IMediator mediator, IMapper mapper)
    : BaseApiController(mediator, mapper)
{
    // GET: api/<v>/<LeaveRequestController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        Result<List<LeaveRequestDto>> result = await _mediator.Send(new GetLeaveRequestListQuery());
        return Ok(result.Data);
    }

    // GET api/<v>/<LeaveRequestController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        Result<LeaveRequestDetailsDto> result = await _mediator.Send(new GetLeaveRequestDetailsQuery(id));

        return result switch
        {
            SuccessResult<LeaveRequestDetailsDto> success => Ok(success.Data),
            NotFoundResult<LeaveRequestDetailsDto> => NotFound()
        };
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

        if (result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Updated));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult.Error)
        };
    }

    // DELETE api/<v>/<LeaveRequestController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        Result result = await _mediator.Send(new DeleteLeaveRequestCommand(id));

        return result switch
        {
            SuccessResult => NoContent(),
            Application.ResultPattern.NotFoundResult => NotFound()
        };
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
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> => NotFound(),
            FailureResult<LeaveRequest> errorResult => BadRequest(errorResult.Error)
        };
    }
}
