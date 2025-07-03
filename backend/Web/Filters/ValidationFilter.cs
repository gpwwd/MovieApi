using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Web.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var nullParameters = context.ActionArguments
            .Where(arg => arg.Value == null)
            .ToList();

        if (nullParameters.Any())
        {
            var details = new ValidationProblemDetails
            {
                Status = 400,
                Title = "One or more parameters are null",
                Detail = $"The following parameters are null: {string.Join(", ", nullParameters.Select(p => p.Key))}"
            };
            context.Result = new BadRequestObjectResult(details);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Status = 422,
                Title = "One or more validation errors occurred",
                Detail = "Please refer to the errors property for additional details"
            };
            context.Result = new UnprocessableEntityObjectResult(details);
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}