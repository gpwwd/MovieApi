using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.Dtos;

namespace MovieApiMvc.Services.Interfaces
{
    public interface IMoviesService
    {
        public Task<List<MovieDto>> GetAll();

        public Task<MovieDto> GetById(Guid id);
        
        public Task PutMovie(Guid id, MovieDto movie);

        public Task DeleteMovie(Guid id);

        public Task<MovieEntity> CreateMovie(MovieDto movie);
    }
}