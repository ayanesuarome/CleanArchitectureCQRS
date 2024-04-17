using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

[Route("api/v{version:apiVersion}")]
[ApiController]
public sealed partial class AuthenticationController(IMediator mediator, IMapper mapper)
    : ControllerBase
{
    public readonly IMediator _mediator = mediator;
    public readonly IMapper _mapper = mapper;
}
