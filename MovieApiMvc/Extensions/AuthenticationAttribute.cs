using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieApiMvc.Extensions;

public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public MyAuthorizeAttribute()
    {
            
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            return;
    
        var user = context.HttpContext.User;
            
        if (user == null || user.Identity?.IsAuthenticated == false)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}