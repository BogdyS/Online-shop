using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.API.Common
{
    public class ExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is { })
            {
                context.Result = new OperationResult
                {
                    Error = context.Exception!
                }.AsActionResult();
            }

            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
