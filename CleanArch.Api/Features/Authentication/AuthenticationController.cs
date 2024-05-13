using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

[AllowAnonymous]
[Route("api/v{version:apiVersion}")]
[ApiController]
public sealed partial class AuthenticationController(ISender sender) : ControllerBase
{
    public readonly ISender _sender = sender;
}
