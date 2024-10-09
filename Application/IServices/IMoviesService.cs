using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.Dtos.UpdateDtos;
using Application.RequestFeatures;

namespace Application.IServices
{
    public interface IMoviesService
    {
        public Task<List<MovieDto>> GetAll();
        public Task<List<MovieDto>> GetWithPaging(MovieParameters movieParams);
        public Task<List<MovieDto>> GetAllWithImages();
        public Task<MovieDto> GetById(Guid id);
        public Task UpdateMovie(Guid id, UpdateMovieDto movie);
        public Task DeleteMovie(Guid id);
        public Task<MovieDto> CreateMovie(PostMovieDto movie);
        public Task<ImageInfoDto> GetImageById(Guid id);
    }
}