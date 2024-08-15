using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Dtos; 

namespace MovieApiMvc.Services.Mappers;

public class DtoToEntity
{
    public static MovieEntity UpdateMovieEntityFromDto(MovieDto movie)
    {
        return new MovieEntity
        {
            Id = movie.Id,
            Name = movie.Name,
            Rating = new RatingEntity
            {
                kp = movie.Rate
            },
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
                kp = movie.Rate
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
            moviesDtoFavMovies.Add(UpdateMovieEntityFromDto(movie));
        }

        foreach(var movie in user.WatchLaterMovies)
        {
            moviesDtoWatchLater.Add(UpdateMovieEntityFromDto(movie));
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
