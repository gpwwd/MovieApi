using System.Text;
using Domain.Entities.UsersEntities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Web.Extensions
{
    public static class AuthenticationExtension
    {

        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
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
                    
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // time after token expiration time start validation
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT_VALID_ISSUER"] ?? 
                            throw new InvalidOperationException("JWT_VALID_ISSUER is not configured"),
                        ValidAudience = configuration["JWT_VALID_AUDIENCE"] ?? 
                            throw new InvalidOperationException("JWT_VALID_AUDIENCE is not configured"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT_SECRET_KEY"] ?? 
                                throw new InvalidOperationException("JWT_SECRET_KEY is not configured")))
                    };
                });
            
            services.AddAuthorization(opt => opt.DefaultPolicy =
                new AuthorizationPolicyBuilder
                        (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            
            return services;
        }

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
        }
    }
}