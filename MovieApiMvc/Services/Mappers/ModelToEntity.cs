using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Models.DomainModels;

namespace MovieApiMvc.Services.Mappers
{
    public static class ModelToEntity
    {
        public static MovieEntity CreateMovieEntityFromModel(Movie movie)
        {
            var movieEntity = new MovieEntity
            {
                Id = movie.Id,
                Name = movie.Name,
                AlternativeName = movie.AlternativeName,
                Type = movie.Type,
                Year = movie.Year,
                Budget = movie.Budget != null ? new BudgetEntity
                {
                    Id = movie.Budget.Id,
                    Currency = movie.Budget.Currency,
                    Value = movie.Budget.Value,
                    MovieId = movie.Id
                } : null,
                Rating = movie.Rating != null ? new RatingEntity
                {
                    Id = movie.Rating.Id,
                    Kp = movie.Rating.Kp,
                    Imdb = movie.Rating.Imdb,
                    FilmCritics = movie.Rating.FilmCritics,
                    RussianFilmCritics = movie.Rating.RussianFilmCritics,
                    MovieId = movie.Id
                } : null,
                MovieLength = movie.MovieLength,
                Genres = movie.Genres?.Select(g => new GenreEntity
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList() ?? new List<GenreEntity>(),
                Countries = movie.Countries?.Select(c => new CountryEntity
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList() ?? new List<CountryEntity>(),
                Top250 = movie.Top250,
                IsSeries = movie.IsSeries,
                FavMovieUsers = movie.FavMovieUsers?.Select(u => new UserEntity
                {
                    Id = u.Id,
                    UserName = u.UserName
                }).ToList() ?? new List<UserEntity>(),
                WatchLaterUsers = movie.WatchLaterUsers?.Select(u => new UserEntity
                {
                    Id = u.Id,
                    UserName = u.UserName
                }).ToList() ?? new List<UserEntity>()
            };

            return movieEntity;
        }
    }
}