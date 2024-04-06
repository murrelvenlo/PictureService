using System.Net;
using Newtonsoft.Json;
using PictureService.Domain.Exceptions;

namespace PictureService.Middleware;

public class ErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandling> _log;

    public ErrorHandling(ILoggerFactory loggerFactory, RequestDelegate next)
    {
        _next = next;
        _log = loggerFactory.CreateLogger<ErrorHandling>();
    }

    public async Task Invoke(HttpContext context /* other scoped dependencies */)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        catch (BusinessLogicException e)
        {
            await HandleBusinessLogicExceptionAsync(context, e);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleBusinessLogicExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        _log.LogError(exception, "Middleware:");
        return context.Response.WriteAsync(JsonConvert.SerializeObject(exception.Message));
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorMessage = exception.Message;

        if (exception is not ApplicationException)
        {
            errorMessage +=
            (
                exception.InnerException != null ?
                    "\r\nInner Exception: " + exception.InnerException.ToString() :
                    string.Empty
            );
        }

        _log.LogError(exception, "Middleware:");
        return context.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));
    }
}