using MovieApiMvc.Models.DomainModels;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services.Mappers
{
    public class DtoToModel
    {
        public static Movie CreateModelFromDto(Guid id, MovieDto movieDto)
        {

            List<Genre> genres = new List<Genre>();
            if(movieDto.Genres != null)
            {
                foreach(var genre in movieDto.Genres)
                {
                    genres.Add(Genre.Create(genre));
                }
            }

            List<Country> countries = new List<Country>();
            if(movieDto.Countries != null)
            {
                foreach(var country in movieDto.Countries)
                {
                    countries.Add(Country.Create(country));
                }
            }

            var movie = Movie.Create(
                name: movieDto.Name,
                alternativeName: movieDto.AlternativeName,
                type: movieDto.Type,
                year: movieDto.Year,
                rating: Rating.Create(movieId: id,
                                    kp: movieDto.Rating.Kp,
                                    imdb: movieDto.Rating.Imdb,
                                    filmCritics: movieDto.Rating.FilmCritics),
                budget: Budget.Create(movieId: id,
                                        currency: movieDto.Budget.Currency,
                                        value: movieDto.Budget.Value),
                genres: genres,
                countries: countries,
                movieLength: movieDto.MovieLength,
                top250: movieDto.Top250,
                isSeries: movieDto.IsSeries
            );

            return movie;
        }
    }
}