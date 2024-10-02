using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MovieApiMvc.DataBaseAccess;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;

namespace MovieApiMvc.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddJWTTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            services.AddAuthorization();
            
            return services;
        }
        /// <summary>
        /// It seems that SignInManager uses cookie authentication
        /// internally and unauthorized requests are redirected to Login page which I have not implemented, hence 404 response.!!!!!!!!!!
        /// !!!!!!!!!!!!!!!!!!!!!!!!! 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<UserEntity, RoleEntity>(o =>
                {
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 10;
                    o.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<MovieDataBaseContext>()
                .AddDefaultTokenProviders();
            
            // services.ConfigureApplicationCookie(options =>
            // {
            //     options.Events.OnRedirectToLogin = context =>
            //     {
            //         context.Response.StatusCode = 401;
            //         return Task.CompletedTask;
            //     };
            // });;
        }
    }
}