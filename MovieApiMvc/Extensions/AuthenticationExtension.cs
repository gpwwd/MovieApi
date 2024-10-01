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
        public static IServiceCollection AddJWTTokenAuthenfication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
                
            });

            return services;
        }
    }
}