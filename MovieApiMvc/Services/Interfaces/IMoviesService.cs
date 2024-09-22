using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.Services.Interfaces
{
    public interface IMoviesService
    {
        public Task<List<MovieDto>> GetAll();
        public Task<List<MovieDto>> GetWithPaging(MovieParameters movieParams);
        public Task<List<MovieDto>> GetAllWithImages();
        public Task<MovieDto> GetById(Guid id);
        
        public Task PutMovie(Guid id, MovieDto movie);

        public Task DeleteMovie(Guid id);

        public Task<MovieDto> CreateMovie(PostMovieDto movie);
        public Task<ImageInfoDto> GetImageById(Guid id);
    }
}