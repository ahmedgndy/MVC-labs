using Microsoft.AspNetCore.Mvc.Filters;

namespace MVc.Filters
{
    public class ActionLoggingFilter : IActionFilter
    {
        private readonly ILogger<ActionLoggingFilter> _logger;


        public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger)
        {
            _logger = logger;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action executed: {Action}", context.ActionDescriptor.DisplayName);
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action executing: {Action}", context.ActionDescriptor.DisplayName);
        }
    }
}
