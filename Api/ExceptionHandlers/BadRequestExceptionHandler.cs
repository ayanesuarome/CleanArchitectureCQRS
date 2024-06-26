﻿using CleanArch.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.ExceptionHandlers;

internal sealed class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// TryHandleAsync attempts to handle the specified exception within the ASP.NET Core pipeline.
    /// </summary>
    /// <returns>
    /// If the exception can be handled, it should return true.
    /// If the exception can't be handled, it should return false
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if(exception is not BadRequestException badRequestException)
        {
            return false;
        }

        ProblemDetails errorDetails = new()
        {
            Title = badRequestException.Message,
            Status = StatusCodes.Status400BadRequest,
            Type = nameof(BadRequestException),
            Detail = badRequestException.Message,
            Extensions = {
                {
                    nameof(badRequestException.Errors),
                    badRequestException.Errors
                }
            }
        };

        var logMessage = JsonConvert.SerializeObject(errorDetails);
        logger.LogError(badRequestException, "Exception ocurred: {Message}", logMessage);

        httpContext.Response.StatusCode = errorDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(errorDetails, cancellationToken);

        return true;
    }
}
