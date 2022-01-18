using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;
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
    public async Task TestGetUserById()
    {
        var user = await _userRepository.GetById(1);
        Console.WriteLine(user.ToString());
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
    public async Task TestGetAllMoviesPurchasedByUser()
    {
        var movies = await _userRepository.GetAllMoviesPurchasedByUser(1);
        foreach (var movie in movies)
        {
            Console.WriteLine(movie.ToString());
        }
    }

    [Test]
    public async Task TestGetAllPurchasesForUser()
    {
        var purchaseModel = await _userService.GetAllPurchasesForUser(1);
        Console.WriteLine(purchaseModel.ToString());
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

    // Test this function again after Favorites database contains values
    [Test]
    public async Task TestFavoriteExists()
    {
        var result1 = await _userService.FavoriteExists(1, 492);
        var result2 = await _userService.FavoriteExists(1, 493);
        Console.WriteLine(result1 + " " + result2);
    }
    
    [Test]
    public async Task TestIsMoviePurchased()
    {
        PurchaseRequestModel p1 = new PurchaseRequestModel
        {
            MovieId = 492,
            UserId = 1
        };
        
        PurchaseRequestModel p2 = new PurchaseRequestModel
        {
            MovieId = 493,
            UserId = 1
        };
        
        var result1 = await _userService.IsMoviePurchased(p1, 1);
        var result2 = await _userService.IsMoviePurchased(p2, 1);
        Console.WriteLine(result1 + " " + result2);
    }

    [Test]
    public async Task TestAddNewPurchase()
    {
        int userId = 1;
        int movieId = 491;
        decimal price = 9.99M;
        var newPurchase = await _userRepository.AddNewPurchase(userId, movieId, price);
        var getNewPurchase = await _userRepository.GetPurchaseByUserAndMovie(userId, movieId);
        Console.WriteLine(getNewPurchase.ToString());
    }

    [Test]
    public async Task TestAddNewFavorite()
    {
        int userId = 1;
        int movieId = 10;
        var favorite = await _userRepository.AddNewFavorite(userId, movieId);
        var query = await _userRepository.GetFavoriteByUserAndMovie(userId, movieId);
        Console.WriteLine(query.ToString());
    }

    [Test]
    public async Task TestRemoveFavorite()
    {
        int userId = 1;
        int movieId = 10;
        await _userRepository.RemoveFavorite(userId, movieId);
        var query = await _userRepository.GetFavoriteByUserAndMovie(userId, movieId);

        if (query == null)
        {
            Console.WriteLine("Removing success");
        }
    }

    [Test]
    public async Task TestAddNewReview()
    {
        var newReview = await _userRepository.AddNewReview(101, 1, 9.9M, "It is awesome!");
        var query = await _userRepository.GetReviewByUserAndMovie(101, 1);
        Console.WriteLine(query.ToString());
    }

    [Test]
    public async Task TestUpdateReview()
    {
        var updatedReview = await _userRepository.UpdateReview(101, 1, 8.9M, "Still awesome");
        Console.WriteLine(updatedReview.ToString());
    }

    [Test]
    public async Task TestDeleteReview()
    {
        var deletedReview = await _userRepository.DeleteReviewByUserAndMovie(101, 1);
        var query = await _userRepository.GetReviewByUserAndMovie(101, 1);
        if (query == null)
        {
            Console.WriteLine("Delete Review Succeed");
        }
    }
}