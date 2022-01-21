using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Servies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService; 
        }
        
        // api/genres
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30GrossingMovies();

            if (!movies.Any())
            {
                // 404
                return NotFound();
            }
            
            // 200
            return Ok(movies);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
    }
}