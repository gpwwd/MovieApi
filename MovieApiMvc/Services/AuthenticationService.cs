using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<IActionResult> RegisterUser(UserDto userForRegistration)
    {
        throw new NotImplementedException();
    }
}