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
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMoviesByPagination([FromQuery] int pageSize = 30, [FromQuery] int page = 1,
            string title = "")
        {
            var movies = await _movieService.GetMoviesByPagination(pageSize, page, title);
            if (movies == null || movies.Count == 0)
            {
                return NotFound("No Movies Was Found");
            }

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

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatingMovies();

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
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
        [Route("genre")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesOfGenre(genreId);

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetReviewsOfMovie(id);

            if (!reviews.Any())
            {
                return NotFound();
            }

            return Ok(reviews);
        }
        
    }
}