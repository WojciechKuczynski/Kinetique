using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Kinetique.Shared.Filters;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.Controller.GetType().Name;
        var action = context.ActionDescriptor.DisplayName;
        _logger.LogInformation("Executing {Controller}.{Action}", controller, action);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var controller = context.Controller.GetType().Name;
        var action = context.ActionDescriptor.DisplayName;
        _logger.LogInformation("Executed {Controller}.{Action}", controller, action);
    }
}