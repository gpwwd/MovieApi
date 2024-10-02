using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;

namespace MovieApiMvc.Services.Interfaces;
public interface IAuthenticationService
{
    public Task<UserDto> Register(UserForRegistrationDto userDto);
    public Task<string> Login(UserLoginDto userDto);
}