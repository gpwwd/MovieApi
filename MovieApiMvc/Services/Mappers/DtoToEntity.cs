using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services.Mappers;

public class DtoToEntity
{
    public static MovieEntity UpdateMovieEntityFromDto(Guid id, MovieDto movie)
    {
        return new MovieEntity
        {
            Id = id,
            Name = movie.Name,
            Rating = new RatingEntity
            {
                Kp = movie.Rating.Kp,
                Imdb = movie.Rating.Imdb,
                FilmCritics = movie.Rating.FilmCritics,
            },
            Budget = new BudgetEntity
            {
                Currency = movie.Budget.Currency,
                Value = movie.Budget.Value,
            },
            AlternativeName = movie.AlternativeName,
            Type = movie.Type,
            Year = movie.Year,
            // genres: genres,
            //     countries: countries,
            MovieLength = movie.MovieLength,
            Top250 = movie.Top250,
            IsSeries = movie.IsSeries
        };
    }

    public static MovieEntity CreateMovieEntityFromDto(MovieDto movie)
    {
        var newId = Guid.NewGuid();
        var movieEntity = new MovieEntity
        {
            Id = newId,
            Name = movie.Name,
            Rating = new RatingEntity
            {
                Id = Guid.NewGuid(),
                MovieId = newId,
                Kp = movie.Rating.Kp
            }
        };
        return movieEntity;
    }

    public static UserEntity CreateUserEntityFromDto(UserDto user, string passwHash)
    {
        List<MovieEntity> moviesDtoFavMovies = new List<MovieEntity>();
        List<MovieEntity> moviesDtoWatchLater = new List<MovieEntity>();

        foreach(var movie in user.FavMovies)
        {
            moviesDtoFavMovies.Add(UpdateMovieEntityFromDto(movie.Id, movie));
        }

        foreach(var movie in user.WatchLaterMovies)
        {
            moviesDtoWatchLater.Add(UpdateMovieEntityFromDto(movie.Id, movie));
        }
        
        return new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = user.UserName,
            PasswHash = passwHash,
            Email = user.Email,
            FavMovies = moviesDtoFavMovies,
            WatchLaterMovies = moviesDtoWatchLater
        };
    }
}
