using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Context;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Middleware;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Services;
using MovieApiMvc.Extensions;

public class Program
{
    public static async Task Main(string[] args)
    {
        

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<MoviesRepository>();
        builder.Services.AddScoped<UsersRepository>();

        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddJWTTokenAuthenfication(builder.Configuration);

        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IUsersService, UsersService>();

        builder.Services.AddDbContext<MovieDataBaseContext>(
            opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("Data Source"));
            });

        var app = builder.Build();
        
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
            pattern: "{controller=Home}/{action=GetAllMovies}/{id?}");

        app.Run();
    }
}

