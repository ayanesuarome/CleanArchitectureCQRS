using CleanArch.Application.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Middlewares;

// This Middleware is activated by convention.
public class ConventionalExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext, ILogger<ConventionalExceptionMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
        Exception exception,
        ILogger<ConventionalExceptionMiddleware> logger)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        ProblemDetails errorDetails;

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails = new ProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Type = nameof(BadRequestException),
                    Detail = badRequestException.InnerException?.Message,
                    Extensions = {
                        {
                            nameof(badRequestException.Errors),
                            badRequestException.Errors
                        }
                    }
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails = new ProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFoundException.InnerException?.Message
                };
                break;
            default:
                errorDetails = new ProblemDetails
                {
                    Title = "Oops! Sorry! Something went wrong. Please contact your administrator.",
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError)
                    //exception.StackTrace
                };
                break;
        }

        logger.LogError(exception, exception.Message);

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorDetails);
    }
}
