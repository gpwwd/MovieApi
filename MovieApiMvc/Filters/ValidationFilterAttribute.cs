using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters;

public class ValidationFilterAttribute : IAsyncActionFilter 
{
public async Task OnActionExecutionAsync(ActionExecutingContext context, 
                                            ActionExecutionDelegate next)
{

    // logic before action goes here

    var action = context.RouteData.Values["action"];
    var controller = context.RouteData.Values["controller"];
    var param = context.ActionArguments
        .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
    if (param is null)
    {
        context.Result = new BadRequestObjectResult($"Object is null. Controller:{controller}, action: {action}");
        return;
    }
    if (!context.ModelState.IsValid)
        context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        

    await next(); 

    // logic after the action goes here
}
}