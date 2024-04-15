using AutoMapper;
using CleanArch.Api.Infrastructure;
using CleanArch.Contracts;
using CleanArch.Contracts.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features;

public sealed partial class AuthenticationController : BaseController
{
    public AuthenticationController(
        IMediator mediator,
        IMapper mapper) : base(mediator, mapper)
    {
    }

    // POST api/<v>/<AuthController>/login
    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(LoginRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return Ok(await _authService.Login(request));
    }
}
