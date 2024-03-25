﻿using CleanArch.Application.Events;
using CleanArch.Application.Extensions;
using CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Models;
using CleanArch.Application.Models.Errors;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotFoundResult = CleanArch.Application.Models.NotFoundResult;

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
            NotFoundResult<LeaveRequestDetailsDto> notfound => NotFound(notfound.Error)
        };
    }

    // POST api/<v>/<LeaveRequestController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand model)
    {
        Result<LeaveRequest> result = await _mediator.Send(model);

        if(result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Created));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> successResult => CreatedAtAction(nameof(Get), new { successResult.Data.Id }),
            ErrorResult<LeaveRequest> errorResult => BadRequest(errorResult.Errors)
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
        
        if(result.IsSuccess)
        {
            await _mediator.Publish(new LeaveRequestEvent(result.Data, LeaveRequestAction.Updated));
        }

        return result switch
        {
            SuccessResult<LeaveRequest> => NoContent(),
            NotFoundResult<LeaveRequest> notFoundResult => NotFound(notFoundResult.Error),
            ErrorResult<LeaveRequest> errorResult => BadRequest(errorResult.Errors)
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
            NotFoundResult notFoundResult => NotFound(notFoundResult.Error)
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
            NotFoundResult<LeaveRequest> notFoundResult => NotFound(notFoundResult.Error),
            ErrorResult<LeaveRequest> errorResult => BadRequest(errorResult.Errors)
        };
    }
}
