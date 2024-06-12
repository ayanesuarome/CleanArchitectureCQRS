using CleanArch.Api.Contracts;
using CleanArch.Api.Features.Authentication.Logins;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/authentication/login
    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(Login.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] Login.Request request, CancellationToken cancellationToken)
    {
        Login.Command command = new(request.Email, request.Password);
        Result<Login.Response> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }
}
