using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/v{version:apiVersion}")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly IMapper _mapper;

    protected BaseApiController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
}
