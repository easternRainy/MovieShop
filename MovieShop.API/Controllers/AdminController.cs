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
        [Route("top-purchased-movies")]
        public async Task<IActionResult> GetTopPurchasedMovies(int pageSize, int page, DateTime fromDate, DateTime endDate)
        {
            if (endDate == null)
            {
                endDate = DateTime.Today;
            }

            if (fromDate == null)
            {
                fromDate = DateTime.Today.AddDays(-90);
            }
            
            var pagedMovies = await _movieService.GetTopPurchasedMoviesByPagination(fromDate, endDate, pageSize, page);
            var movies = pagedMovies.Data;

            if (pagedMovies == null)
            {
                return NotFound();
            }

            return Ok(pagedMovies);
        }

        [HttpPost]
        [Route("movie")]
        public async Task CreateMovie([FromBody] MovieCreateRequestModel model)
        {
            await _movieService.CreateMovie(model);
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovieDetails([FromBody] MovieCreateRequestModel model)
        {
            var success = await _movieService.UpdateMovieDetails(model);

            return Ok(success);
        }

    }
}