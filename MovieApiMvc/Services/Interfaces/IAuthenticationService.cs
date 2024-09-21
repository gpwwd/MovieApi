using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;

namespace MovieApiMvc.Services.Interfaces;
public interface IAuthenticationService
{
    Task<IActionResult> RegisterUser(UserDto userForRegistration);
}