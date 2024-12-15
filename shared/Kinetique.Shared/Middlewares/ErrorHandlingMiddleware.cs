using Kinetique.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Kinetique.Shared.Middlewares;

internal class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleErrorAsync(context, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = 400;
        if (exception is KinetiqueException kinetiqueException)
        {
            await context.Response.WriteAsJsonAsync(new
                { code = kinetiqueException.Code, message = kinetiqueException.ExceptionMessage });
            return;
        }
        
        await context.Response.WriteAsJsonAsync(new { code = "Critical error", message = "There was some error" });
    }
}