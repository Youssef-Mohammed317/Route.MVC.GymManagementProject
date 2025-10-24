using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GymManagement.PL.Common
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Log the error (to console, file, or whatever logging provider)
            _logger.LogError(exception, "Unhandled exception occurred.");

            // Detect if it's an API request or MVC request
            var isApi = context.HttpContext.Request.Path.Value?.StartsWith("/api") == true;

            if (isApi)
            {
                // Return JSON for API calls
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "An unexpected error occurred.",
                    error = exception.Message
                })
                {
                    StatusCode = 500
                };
            }
            else
            {
                // Return a friendly error page for MVC
                var result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml"
                };
                result.ViewData["ExceptionMessage"] = exception.Message;
                result.ViewData["StackTrace"] = exception.StackTrace;

                context.Result = result;
            }

            context.ExceptionHandled = true; // mark as handled
        }
    }
}
