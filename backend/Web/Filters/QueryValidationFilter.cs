using Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class QueryValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var parameters = context.ActionArguments["movieRatingParameters"] as MovieRatingParameters;
        
        if(parameters is null)
        {
            context.Result = new BadRequestObjectResult($"Query Request is null. Controller:{context.Controller}, " +
                                                        $"action: {context.RouteData.Values["action"]}, ");
            return;
        }
        
        if(!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}