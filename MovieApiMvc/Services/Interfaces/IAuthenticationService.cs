using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;

namespace MovieApiMvc.Services.Interfaces;
public interface IAuthenticationService
{
    public Task<UserDto> Register(UserForRegistrationDto userDto);
    public Task<bool> ValidateUser(UserLoginDto userLoginDto);
    public Task<TokenDto> CreateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}