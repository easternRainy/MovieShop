using System;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using Intrastructure.Data;
using Intrastructure.Repositories;
using NUnit.Framework;
using ApplicationCore.Entities;
using Infrastructure.Services;

namespace TestMovieRepositoryAndService;

public class Tests
{
    
    private MovieShopDbContext _dbContext;
    private IMovieRepository _movieRepository;
    private IMovieService _movieService;
    
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Setting up Testing Environment");
        string connection =
            "Server=DESKTOP-E633351,1433;Database=MovieShop;User Id=SA;Password=Guest-Turing;MultipleActiveResultSets=True;";
        _dbContext = new MovieShopDbContext(connection);
        _movieRepository = new MovieRepository(_dbContext);
        _movieService = new MovieService(_movieRepository);
        Console.WriteLine("Testing Environment Setting Succeeded");
        
    }

    [Test]
    public async Task TestGetMovieById()
    {
        var movie = await _movieRepository.GetById(50);
        Console.WriteLine(movie.ToString());
    }

    [Test]
    public async Task TestGet30HighestGrossingMovies()
    {
        var top30Movies = await _movieRepository.Get30HighestGrossingMovies();
        foreach (Movie movie in top30Movies)
        {
            Console.WriteLine(movie.ToString());
        }
    }

    [Test]
    public async Task TestGetTop30GrossingMovies()
    {
        var top30GrossingMovies = await _movieService.GetTop30GrossingMovies();
        foreach (var movie in top30GrossingMovies)
        {
            Console.WriteLine(movie.ToString());
        }
    }

    [Test]
    public async Task TestGetMovieDetails()
    {
        var movie = await _movieService.GetMovieDetails(20);
        Console.WriteLine(movie.ToString());
    }
}