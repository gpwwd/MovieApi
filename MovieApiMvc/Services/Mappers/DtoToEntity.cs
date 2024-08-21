using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Dtos; 

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
                kp = movie.RatingKp,
                imdb = movie.RatingImdb,
                filmCritics = movie.RatingFilmCritics
            },
            Budget = new BudgetEntity
            {
                Currency = movie.BudgetCurrency,
                Value = movie.BudgetValue ?? -1,
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
                kp = movie.RatingKp
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
