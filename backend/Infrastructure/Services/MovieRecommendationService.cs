using Application.IServices;
using Domain.Entities.MovieEntities;
using Domain.Exceptions.NotFoundExceptions;
using Infrastructure.ExternalApi;

namespace Infrastructure.Services;

public class MovieRecommendationService : IMovieRecommendationService
{
    private readonly OpenRouterService _openRouterService;
    private readonly IUsersService _userService;

    public MovieRecommendationService(
        OpenRouterService openRouterService,
        IUsersService userService)
    {
        _openRouterService = openRouterService;
        _userService = userService;
    }

    public async Task<string> GetRecommendationsForUser(Guid userId)
    {
        var user = await _userService.GetEntityById(userId);
        
        if (user.FavMovies == null || !user.FavMovies.Any())
        {
            throw new InvalidOperationException("User must have favorite movies to get recommendations");
        }

        return await _openRouterService.GetMovieRecommendations(user.FavMovies);
    }

    public async Task<string> AnalyzeUserPreferences(Guid userId)
    {
        var user = await _userService.GetEntityById(userId);
        
        if (user.FavMovies == null || !user.FavMovies.Any())
        {
            throw new InvalidOperationException("User must have favorite movies to analyze preferences");
        }

        return await _openRouterService.AnalyzeUserPreferences(user.FavMovies);
    }
} 