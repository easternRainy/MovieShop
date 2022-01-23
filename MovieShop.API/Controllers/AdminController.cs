using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public AdminController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetTopPurchasedMovies(int pageSize, int page)
        {
            var pagedMovies = await _movieService.GetTopPurchasedMoviesByPagination(pageSize, page);
            var movies = pagedMovies.Data;

            // if (!movies.Any())
            // {
            //     return NotFound();
            // }
            // else
            // {
            //     return Ok(movies);
            // }

            if (pagedMovies == null)
            {
                return NotFound();
            }

            return Ok(pagedMovies);
        }

    }
}