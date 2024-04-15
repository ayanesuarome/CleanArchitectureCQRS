using CleanArch.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/<AuthController>/login
    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(LoginRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return Ok(await _authService.Login(request));
    }
}
