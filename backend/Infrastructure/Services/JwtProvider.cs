using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(List<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT_VALID_ISSUER"],
            audience: _configuration["JWT_VALID_AUDIENCE"],
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddHours(2)
        );
                
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
