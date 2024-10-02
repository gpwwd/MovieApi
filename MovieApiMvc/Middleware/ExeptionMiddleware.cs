using MovieApiMvc.Models.Dtos;
using System.Net;
using MovieApiMvc.ErrorHandling;
using MovieApiMvc.ErrorHandling.AuthenticationExtensions;
using MovieApiMvc.ErrorHandling.NotFoundExceptions;

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
        
        ExceptionResponseDevelopment response = exception switch
        {
            ApplicationException => new ExceptionResponseDevelopment(HttpStatusCode.BadRequest, "Application exception occurred.", exception.StackTrace),
            NotFoundException => new ExceptionResponseDevelopment(HttpStatusCode.NotFound, exception.Message, exception.StackTrace),
            MyAuthenticationException => new ExceptionResponseDevelopment(HttpStatusCode.Unauthorized, exception.Message, exception.StackTrace),
            UnauthorizedAccessException => new ExceptionResponseDevelopment(HttpStatusCode.Unauthorized, exception.Message, exception.StackTrace),
            _ => new ExceptionResponseDevelopment(HttpStatusCode.InternalServerError, exception.Message, exception.StackTrace)
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        if(_environment.IsDevelopment())
            await context.Response.WriteAsJsonAsync(response);
        if(_environment.IsProduction())
            await context.Response.WriteAsJsonAsync((ExceptionResponse)response);
    }
}

public static class ExeptionExtension
{
    public static IApplicationBuilder UseMyExeptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        return app.UseMiddleware<ExceptionMiddleware>(env);
    }
}