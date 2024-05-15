﻿using CleanArch.Contracts.Identity;
using CleanArch.Api.Features.Authentication.CreateUsers;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Api.Contracts;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController
{
    // POST api/<v>/authentication/register
    [HttpPost(ApiRoutes.Authentication.Register)]
    [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request, CancellationToken cancellationToken)
    {
        CreateUser.Command command = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        Result<RegistrationResponse> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
