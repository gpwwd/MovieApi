using Application.ExternalApiInterfaces;
using Application.IManagers;
using Application.IServices;
using Application.Mappers;
using Domain.Entities.MovieEntities;
using Infrastructure.Database;
using Infrastructure.ExternalApi;
using Infrastructure.Managers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Web.Extensions;
using Web.Filters;
using Web.Middleware;
using DotNetEnv;
using System.IO;

namespace Web;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var envPath = Path.Combine(AppContext.BaseDirectory, ".env");
        Console.WriteLine($"Loading .env from: {envPath}");
        Env.Load(envPath);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        builder.Services.AddAutoMapper(typeof(MovieMapperProfile), typeof(UserMapperProfile));
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        builder.Services.AddControllers(); 
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();
        
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<ValidationFilter>();
        builder.Services.AddScoped<QueryValidationFilter>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        
        builder.Services.AddDbContext<MovieDataBaseContext>();
        builder.Services.AddJwtTokenAuthentication(builder.Configuration);
        builder.Services.ConfigureIdentity();

        builder.Services.AddScoped<IKinopoiskApiService, KinopoiskApiService>();
        builder.Services.AddSingleton(typeof(Deserializer));
        builder.Services.AddSingleton(typeof(ResponseFetcher));
        
        var app = builder.Build();

        app.UseRouting();
        
        app.UseCors("AllowAll");
        app.UseMyExeptionHandling(builder.Environment);
        
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=GetAllMovies}/{id?}"
        );

        app.Run();
    }


}