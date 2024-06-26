﻿using CleanArch.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.ExceptionHandlers;

internal sealed class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
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
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        logger.LogError(notFoundException, "Exception ocurred: {Message}", exception.Message);

        ProblemDetails errorDetails = new()
        {
            Title = notFoundException.Message,
            Status = StatusCodes.Status404NotFound,
            Type = nameof(NotFoundException),
            Detail = notFoundException.Message
        };

        httpContext.Response.StatusCode = errorDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(errorDetails, cancellationToken);

        return true;
    }
}
