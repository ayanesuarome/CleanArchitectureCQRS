using CleanArch.Contracts;
using CleanArch.Contracts.Identity;
using CleanArch.Api.Features.Users.CreateUsers;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/<AuthController>/register
    [HttpPost(ApiRoutes.Authentication.Register)]
    [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        CreateUser.Command command = _mapper.Map<CreateUser.Command>(request);
        Result<RegistrationResponse> result = await _mediator.Send(command);

        return result switch
        {
            SuccessResult<RegistrationResponse> successResult => Ok(successResult.Data),
            FailureResult<RegistrationResponse> errorResult => BadRequest(errorResult.Error)
        };
    }
}
