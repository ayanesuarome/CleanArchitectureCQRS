using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

[Route("api/v{version:apiVersion}")]
[ApiController]
public sealed partial class AuthenticationController(IMediator mediator) : ControllerBase
{
    public readonly IMediator _mediator = mediator;
}
