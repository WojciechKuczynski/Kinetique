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
        var err = new { code = exception.GetType().Name, message = exception.Message };
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(err);
    }
}