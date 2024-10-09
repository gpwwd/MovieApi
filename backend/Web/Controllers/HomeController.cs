using Application.IServices;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesService _moviesService;
        public HomeController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        // [HttpGet]
        // public async Task<ActionResult<List<MovieDto>>> GetAllMovies()
        // {
        //     var movieDTOs = await _moviesService.GetAll();
        //     return Ok(movieDTOs);
        // }
    }
}