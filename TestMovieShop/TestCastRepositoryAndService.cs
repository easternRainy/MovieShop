using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using Intrastructure.Data;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Infrastructure.Services;
using Intrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic.CompilerServices;

namespace TestMovieRepositoryAndService;

public class TestCastRepositoryAndService
{
    private MovieShopDbContext _dbContext;
    private ICastRepository _castRepository;
    private ICastService _castService;
    
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Setting up Testing Environment");
        string connection =
            "Server=DESKTOP-E633351,1433;Database=MovieShop;User Id=SA;Password=Guest-Turing;MultipleActiveResultSets=True;";
        _dbContext = new MovieShopDbContext(connection);
        _castRepository = new CastRepository(_dbContext);
        _castService = new CastService(_castRepository);
        Console.WriteLine("Testing Environment Setting Succeeded");
        
    }

    [Test]
    public async Task TestGetMovieIdsByCastId()
    {
        var movieIds = _castRepository.GetMovieIdsById(1);

        foreach (int id in movieIds.Result)
        {
            Console.WriteLine(id);
        }
    }

    [Test]
    public async Task TestGetMoviesByCastId()
    {
        var movieCards = await _castRepository.GetMoviesById(1);

        foreach (var card in movieCards)
        {
            Console.WriteLine(card.ToString());
        }
    }

}