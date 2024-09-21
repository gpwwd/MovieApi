using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services.Interfaces;
public interface IAuthenticationService
{
    Task<IActionResult> RegisterUser(UserDto userForRegistration);
}