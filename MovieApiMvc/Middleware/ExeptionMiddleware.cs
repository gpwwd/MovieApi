using MovieApiMvc.ErrorHandling;
using Newtonsoft.Json;

namespace MovieApiMvc.Middleware;
public class ExeptionMiddleware
{  
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExeptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _environment = env;
    }
    
    public async Task InvokeAsync(HttpContext context){
        try
        {
                await _next.Invoke(context);
        }
        catch(Exception ex) 
        {
            MyExeption? response = null;
            if(_environment.IsDevelopment())
            {
                response =  new MyExeption(context.Response.StatusCode, ex.Message, ex.StackTrace);
            }
            else
            {
                response = new MyExeption(context.Response.StatusCode, "Internal Server Error");
            }

            var json = JsonConvert.SerializeObject(response.ToString());
            await context.Response.WriteAsync(json);
        }
    }
}

public static class ExeptionExtension
{
    public static IApplicationBuilder UseMyExeptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        return app.UseMiddleware<ExeptionMiddleware>(env);
    }
}