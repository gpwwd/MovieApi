using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Dtos; 

namespace MovieApiMvc.Services.Mappers;

public class EntityToDto
{
    public static MovieDto CreateMovieDtoFromEntity(MovieEntity? movieEntity)
    {
        return new MovieDto()
        {
            Name = movieEntity.Name,
            Rate = movieEntity.Rating.kp,
            Type = movieEntity.Type,
            Year = movieEntity.Year,
            Id = movieEntity.Id
        };
    }

    public static UserDto CreateUserDtoFromEntity(UserEntity? userEntity)
    {
        List<MovieDto> moviesDtoFavMovies = new List<MovieDto>();
        List<MovieDto> moviesDtoWatchLater = new List<MovieDto>();

        foreach(var movie in userEntity.FavMovies)
        {
            moviesDtoFavMovies.Add(CreateMovieDtoFromEntity(movie));
        }

        foreach(var movie in userEntity.WatchLaterMovies)
        {
            moviesDtoWatchLater.Add(CreateMovieDtoFromEntity(movie));
        }
        
        return new UserDto()
        {
            Id = userEntity.Id, 
            UserName = userEntity.UserName,    
            Password = userEntity.PasswHash,
            Email = userEntity.Email,
            FavMovies = moviesDtoFavMovies,
            WatchLaterMovies = moviesDtoWatchLater
        };
    }

}
