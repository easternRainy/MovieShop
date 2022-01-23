using ApplicationCore.Contracts.Servies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    // GET

    // [HttpGet]
    // [Route("")]
    // // http://localhost:.../api/movies?pagesize=30&page=2&title=ave
    // public async Task<IActionResult> GetMoviesByPagination([FromQuery] int pageSize = 30, [FromQuery] int page = 1,
    //     string title = "")
    // {
    //     var movies = await _movieService.GetMoviesByPagination(pageSize, page, title);
    //     return View(movies);
    // }
    
    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await _movieService.GetMovieDetails(id);
        
        return View(movieDetails);
    }

    [HttpGet]
    public async Task<IActionResult> Genre(int id)
    {
        int genreId = id;
        var movieModels = await _movieService.GetMoviesOfGenre(genreId);
        
        return View(movieModels);
    }
}