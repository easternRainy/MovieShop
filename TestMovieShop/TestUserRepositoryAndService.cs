using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using Infrastructure.Services;
using Intrastructure.Data;
using Intrastructure.Repositories;

namespace TestMovieRepositoryAndService;

[TestFixture]
public class TestUserRepositoryAndService
{
    private MovieShopDbContext _dbContext;
    private IUserRepository _userRepository;
    private IPurchaseRepository _purchaseRepository;
    private IUserService _userService;
    
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Setting up Testing Environment");
        string connection =
            "Server=DESKTOP-E633351,1433;Database=MovieShop;User Id=SA;Password=Guest-Turing;MultipleActiveResultSets=True;";
        _dbContext = new MovieShopDbContext(connection);
        _userRepository = new UserRepository(_dbContext);
        _purchaseRepository = new PurchaseRepository(_dbContext);
        _userService = new UserService(_userRepository);
        Console.WriteLine("Testing Environment Setting Succeeded");
    }

    [Test]
    public async Task TestGetAllPurchasesOfUser()
    {
        var purchases = await _userRepository.GetAllPurchasesOfUser(1);
        foreach (var purchase in purchases)
        {
            Console.WriteLine(purchase.ToString());
        }
    }

    [Test]
    public async Task TestGetPurchaseByUserAndMovie()
    {
        var purchase = await _userRepository.GetPurchaseByUserAndMovie(1, 492);
        if (purchase != null)
        {
            Console.WriteLine(purchase.ToString());
        }
        else
        {
            Console.WriteLine("User did not buy this movie");
        }
    }
    
    [Test]
    public async Task TestGetFavoriteByUserAndMovie()
    {
        var favorite = await _userRepository.GetFavoriteByUserAndMovie(1, 492);
        if (favorite != null)
        {
            Console.WriteLine(favorite.ToString());
        }
        else
        {
            Console.WriteLine("User did not favorite this movie");
        }
    }
}