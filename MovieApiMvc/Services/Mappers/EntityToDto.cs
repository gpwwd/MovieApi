using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Dtos; 

namespace MovieApiMvc.Services.Mappers;

public class EntityToDto
{
    public static MovieDto CreateMovieDtoFromEntity(MovieEntity movieEntity)
    {
        return new MovieDto()
        {
            Name = movieEntity.Name,
            AlternativeName = movieEntity.AlternativeName,
            Rate = movieEntity.Rating?.kp,
            Type = movieEntity.Type,
            Year = movieEntity.Year,
            BudgetValue = movieEntity.Budget?.Value ?? null,
            MovieLength = movieEntity.MovieLength,
            Id = movieEntity.Id,
            IsSeries = movieEntity.IsSeries,
            Top250 = movieEntity.Top250,
            Countries = movieEntity.Countries?.Select(c => c.Name).ToList() ?? null,
            Genres = movieEntity.Genres?.Select(c => c.Name).ToList() ?? null
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
