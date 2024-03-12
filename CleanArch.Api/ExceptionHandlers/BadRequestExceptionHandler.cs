using CleanArch.Api.Models;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanArch.Api.ExceptionHandlers;

internal sealed class BadRequestExceptionHandler(IAppLogger<BadRequestExceptionHandler> logger) : IExceptionHandler
{
    private readonly IAppLogger<BadRequestExceptionHandler> _logger = logger;

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

        _logger.LogError(badRequestException, "Exception ocurred: {Message}", exception.Message);

        CustomProblemDetails errorDetails = new()
        {
            Title = badRequestException.Message,
            Status = StatusCodes.Status400BadRequest,
            Type = nameof(BadRequestException),
            Detail = badRequestException.Message,
            Errors = badRequestException.ValidationErrors
        };

        httpContext.Response.StatusCode = errorDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(errorDetails, cancellationToken);

        return true;
    }
}
