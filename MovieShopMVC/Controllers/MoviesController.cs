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
    
    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await _movieService.GetMovieDetails(id);
        
        return View(movieDetails);
    }

    [HttpGet]
    public async Task Genre()
    {
        throw new NotImplementedException();
    }
}