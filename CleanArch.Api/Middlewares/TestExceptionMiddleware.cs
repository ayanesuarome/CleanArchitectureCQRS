using CleanArch.Api.Models;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CleanArch.Api.Middlewares;

public class TestExceptionMiddleware(RequestDelegate next, IAppLogger<ExceptionMiddleware> logger)
    : IMiddleware
{
    private readonly RequestDelegate _next = next;
    private readonly IAppLogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
        Exception exception,
        IAppLogger<ExceptionMiddleware> logger)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails errorDetails;

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Type = nameof(BadRequestException),
                    Detail = badRequestException.InnerException?.Message,
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails = new CustomProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFoundException.InnerException?.Message
                };
                break;
            default:
                errorDetails = new CustomProblemDetails
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
