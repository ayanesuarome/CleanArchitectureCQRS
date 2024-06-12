using CleanArch.Api.Features.Authentication.CreateUsers;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Api.Contracts;
using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/authentication/register
    [HttpPost(ApiRoutes.Authentication.Register)]
    [ProducesResponseType(typeof(CreateUser.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] CreateUser.Request request, CancellationToken cancellationToken)
    {
        CreateUser.Command command = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        Result<CreateUser.Response> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
