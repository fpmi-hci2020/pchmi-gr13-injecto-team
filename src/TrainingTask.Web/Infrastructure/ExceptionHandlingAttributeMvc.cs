using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TrainingTask.Web.Infrastructure
{
    public class ExceptionHandlingAttributeMvc : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ExceptionHandlingAttributeMvc(ILogger<ExceptionHandlingAttributeMvc> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            context.Result = new RedirectToActionResult("Error", "Home", null);
        }
    }
}