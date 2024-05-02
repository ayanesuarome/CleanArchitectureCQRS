using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Contracts;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveTypes;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public sealed class LeaveTypesV2Controller(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET api/<v>/<LeaveTypesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeaveTypeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        Result<LeaveTypeDetailDto> result = await _mediator.Send(new GetLeaveTypeDetail.Query(id));

        return result switch
        {
            SuccessResult<LeaveTypeDetailDto> success => Ok(success.Value),
            NotFoundResult<LeaveTypeDetailDto> => NotFound()
        };
    }
}
