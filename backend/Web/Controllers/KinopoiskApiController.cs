using Application.ExternalApiInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/kinopoisk")]
[ApiController]
public class KinopoiskApiController
{
    private readonly IKinopoiskApiService _kinopoiskApiService;

    public KinopoiskApiController(IKinopoiskApiService kinopoiskApiService)
    {
        _kinopoiskApiService = kinopoiskApiService;
    }
    
    [HttpGet]
    public async Task GetMoviesData()
    {
        var data = await _kinopoiskApiService.GetMoviesData();
    }
}