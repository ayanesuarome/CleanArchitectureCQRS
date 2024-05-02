using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

[Route("api/v{version:apiVersion}")]
[ApiController]
public sealed partial class AuthenticationController(ISender sender) : ControllerBase
{
    public readonly ISender _sender = sender;
}
