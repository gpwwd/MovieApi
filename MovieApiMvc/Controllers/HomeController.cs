using Microsoft.AspNetCore.Mvc;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Services.Interfaces;

namespace MovieApiMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesService _moviesService;
        public HomeController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetAllMovies()
        {
            var movieDTOs = await _moviesService.GetAll();
            return Ok(movieDTOs);
        }
    }
}