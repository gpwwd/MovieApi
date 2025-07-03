using Application.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/recommendations")]
[ApiController]
public class MovieRecommendationsController : ControllerBase
{
    private readonly IMovieRecommendationService _recommendationService;

    public MovieRecommendationsController(IMovieRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("analyze")]
    public async Task<ActionResult<string>> AnalyzePreferences()
    {
        var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var analysis = await _recommendationService.AnalyzeUserPreferences(Guid.Parse(userId));
        return Ok(analysis);
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetRecommendations()
    {
        var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var recommendations = await _recommendationService.GetRecommendationsForUser(Guid.Parse(userId));
        return Ok(recommendations);
    }
} 