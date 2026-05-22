using EduLink.Domain.Exceptions;
using Serilog.Core;

namespace EduLink.Api.Middlewares;

public class GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger) : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }

        catch (NotFoundException notFound)
        {

            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);

            logger.LogWarning(notFound.Message);
        }

        catch (ForbidExeption)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access Forbiden!");
        }

        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("something went Wrong!");
        }
    }

    //private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
    //{
    //    logger.LogError(exception, exception.Message);

    //    int statusCode = StatusCodes.Status500InternalServerError;
    //    string message = "Internal Server Error";

    //    if (exception is BadRequestException badRequest)
    //    {
    //        statusCode = StatusCodes.Status400BadRequest;
    //        message = badRequest.Message;
    //    }
    //    else if (exception is NotFoundException notFound)
    //    {
    //        statusCode = StatusCodes.Status404NotFound;
    //        message = notFound.Message;
    //    }
    //    //else if (exception is DuplicateUserException duplicate)
    //    //{
    //    //    statusCode = StatusCodes.Status409Conflict;
    //    //    message = duplicate.Message;
    //    //}
    //    // here add next exeption

    //    context.Response.ContentType = "application/json";
    //    context.Response.StatusCode = statusCode;

    //    var result = new
    //    {
    //        error = message,
    //        status = statusCode
    //    };

    //    return context.Response.WriteAsJsonAsync(result);
    //}


}

