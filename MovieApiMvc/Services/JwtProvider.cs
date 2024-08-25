using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.Services;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken()//can use User params later as a params for generating token (Claims)
    {
        var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                signingCredentials: new SigningCredentials (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])), 
                                SecurityAlgorithms.HmacSha512),
                expires: DateTime.UtcNow.AddMinutes(30)
        );
                
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
