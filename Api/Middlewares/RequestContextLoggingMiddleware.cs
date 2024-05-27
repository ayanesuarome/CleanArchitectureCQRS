using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace CleanArch.Api.Middlewares;

public class RequestContextLoggingMiddleware : IMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        {
            return next.Invoke(context);
        }
    }

    private object GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            CorrelationIdHeader,
            out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
