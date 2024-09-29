using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Extensions;
using MovieApiMvc.Filters;
using MovieApiMvc.Middleware;
using MovieApiMvc.Services;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Services.Mappers;
using AutoMapper;
using Newtonsoft.Json;

namespace MovieApiMvc;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        // Add services to the container.
        builder.Services.AddAutoMapper(typeof(MovieMapperProfile), typeof(UserMapperProfile));
        
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<MoviesRepository>();
        builder.Services.AddScoped<UsersRepository>();

        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddJWTTokenAuthenfication(builder.Configuration);

        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        
        builder.Services.AddScoped<ValidationFilterAttribute>();

        builder.Services.AddDbContext<MovieDataBaseContext>(
            opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("Data Source"));
            });

        var app = builder.Build();  
        
        app.UseCors("AllowAll");
        if(app.Environment.IsDevelopment()) 
            app.UseDeveloperExceptionPage();
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