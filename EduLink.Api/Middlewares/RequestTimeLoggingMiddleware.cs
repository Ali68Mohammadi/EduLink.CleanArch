using System.Diagnostics;

namespace EduLink.Api.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWach = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWach.Stop();

        if (stopWach.ElapsedMilliseconds / 1000 > 4)
        {
            logger.LogInformation("request [{verb}] at {Path} took {Time} ms",
                context.Request.Method,
                context.Request.Path,
                stopWach.ElapsedMilliseconds);
        }

    }


}
