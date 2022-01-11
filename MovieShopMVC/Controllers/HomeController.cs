using System.Diagnostics;
using ApplicationCore.Contracts.Servies;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IMovieService _movieService;
    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // await any method that returns Task
        var movies = await _movieService.GetTop30GrossingMovies();
        ViewBag.TotalMovies = movies.Count;
        return View(movies);
    }

    // http://localhost:PORT/Home/TopMovies
    [HttpGet]
    public IActionResult TopMovies()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}