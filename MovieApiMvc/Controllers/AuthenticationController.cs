﻿using AutoMapper;
using MovieApiMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.PostDtos;

namespace MovieApiMvc.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    /// <summary>
    /// Use "UserManager" class later
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserForRegistrationDto user)
    {
        var createdEntity = await _authenticationService.Register(user);
        return Created("users/register", createdEntity);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {   
        var token = await _authenticationService.Login(userLoginDto);
        return Ok( token );
    }
}