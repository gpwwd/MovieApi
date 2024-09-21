using MovieApiMvc.Models.Dtos;
using System.Net;
using MovieApiMvc.ErrorHandling;

namespace MovieApiMvc.Middleware;
public class ExceptionMiddleware
{  
    private readonly RequestDelegate _next;
    //private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;
    public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
        //_logger = logger; use later
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    { 
        //More log stuff        

        ExceptionResponse response = exception switch
        {
            ApplicationException => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
            NotFoundException => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
            UnauthorizedAccessException => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}

public static class ExeptionExtension
{
    public static IApplicationBuilder UseMyExeptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        return app.UseMiddleware<ExceptionMiddleware>(env);
    }
}