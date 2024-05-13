﻿using CleanArch.Contracts;
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
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        Login.Login.Command command = new(request.Email, request.Password);
        Result<TokenResponse> response = await _sender.Send(command, cancellationToken);

        return response switch
        {
            SuccessResult<TokenResponse> successResult => Ok(successResult.Value),
            FailureResult<TokenResponse> errorResult => BadRequest(errorResult)
        };
    }
}
