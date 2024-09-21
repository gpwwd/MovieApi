using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities.UsersEntities;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services.Mappers;

public class EntityToDto
{
    public static MovieDto CreateMovieDtoFromEntity(MovieEntity movieEntity)
    {
        return new MovieDto()
        {
            Name = movieEntity.Name,
            AlternativeName = movieEntity.AlternativeName,
            Rating = new RatingDto
                {
                    FilmCritics = movieEntity.Rating.FilmCritics,
                    Imdb = movieEntity.Rating.Imdb,
                    Kp = movieEntity.Rating.Kp,
                    Id = movieEntity.Rating.Id,
                },
            Type = movieEntity.Type,
            Year = movieEntity.Year,
            Budget = new BudgetDto
                {
                    Currency = movieEntity.Budget.Currency,
                    Id = movieEntity.Budget.Id,
                    MovieId =  movieEntity.Budget.MovieId,
                    Value = movieEntity.Budget.Value,
                },
            MovieLength = movieEntity.MovieLength,
            Id = movieEntity.Id,
            IsSeries = movieEntity.IsSeries,
            Top250 = movieEntity.Top250,
            Countries = movieEntity.Countries?.Select(c => c.Name).ToList() ?? null,
            Genres = movieEntity.Genres?.Select(c => c.Name).ToList() ?? null,
            ImageInfo = new ImageInfoDto
                        {
                            Id = movieEntity.ImageInfoEntity.Id,
                            MovieId = movieEntity.ImageInfoEntity.MovieId,
                            Urls = movieEntity.ImageInfoEntity?.Urls ?? null,
                            PreviewUrls = movieEntity.ImageInfoEntity?.PreviewUrls ?? null,
                        },
            Description = movieEntity.Description,
            ShortDescription = movieEntity.ShortDescription
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
