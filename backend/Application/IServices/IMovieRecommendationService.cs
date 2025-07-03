using Domain.Entities.MovieEntities;

namespace Application.IServices;

public interface IMovieRecommendationService
{
    Task<string> GetRecommendationsForUser(Guid userId);
    Task<string> AnalyzeUserPreferences(Guid userId);
} 