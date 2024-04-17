using CleanArch.Contracts;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/authentication/login
    [HttpPost(ApiRoutes.Authentication.Login)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Login.Login.Command command = _mapper.Map<Login.Login.Command>(request);
        Result<TokenResponse> response = await _mediator.Send(command);

        return response switch
        {
            SuccessResult<TokenResponse> successResult => Ok(successResult.Data),
            FailureResult<TokenResponse> errorResult => BadRequest(errorResult)
        };
    }
}
