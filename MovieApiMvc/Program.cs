using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess;
using MovieApiMvc.DataBaseAccess.Repositories;
using MovieApiMvc.Extensions;
using MovieApiMvc.Filters;
using MovieApiMvc.Middleware;
using MovieApiMvc.Services;
using MovieApiMvc.Services.Interfaces;
using MovieApiMvc.Services.Mappers;

namespace MovieApiMvc;

public abstract class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

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
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();
        
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

        builder.Services.AddDbContext<MovieDataBaseContext>(
            opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("Data Source"));
            });
        builder.Services.AddJWTTokenAuthentication(builder.Configuration);
        builder.Services.ConfigureIdentity();
        
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