using CleanArch.Contracts;
using CleanArch.Contracts.Identity;
using CleanArch.Api.Features.Authentication.CreateUsers;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/authentication/register
    [HttpPost(ApiRoutes.Authentication.Register)]
    [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        CreateUser.Command command = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        Result<RegistrationResponse> result = await _sender.Send(command);

        return result switch
        {
            SuccessResult<RegistrationResponse> successResult => Ok(successResult.Value),
            FailureResult<RegistrationResponse> errorResult => BadRequest(errorResult.Error)
        };
    }
}
