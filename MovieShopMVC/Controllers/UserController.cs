using System.Security.Claims;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    private int GetUserId()
    {
        return Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
    }
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    
    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        // call user service with login-ed user id and get the movies user purchased from Purchase table
        // that will give list of movies user purchased and should return a View that will show MovieCards and should use MovieCard partial view.
        var userId = GetUserId();
        var model = await _userService.GetAllPurchasesForUser(userId);
        
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        // give list of movies user Favorited and should return a View that will show MovieCards and should use MovieCard partial view.

        var userId = GetUserId();
        var model = await _userService.GetAllFavoritesForUser(userId);
        return View(model);
    }

    [HttpGet]
    //for user to buy a movie, when user click on Purchase button in Movie Details Page Purchase Confirmation Popup
    public async Task<IActionResult> Buy()
    {
        throw new NotImplementedException();
        
    }

    [HttpPost]
    public async Task<IActionResult> Buy(PurchaseRequestModel model)
    {
        int userId = GetUserId();
        var purchaseSucceed = await _userService.PurchaseMovie(model, userId);

        return RedirectToAction("Purchases");
    }
    
    // for user to add a new Review, when user clicks on Review button in Movie Details Page and Review Confirmation Popup
    [HttpGet]
    public async Task Review()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> Review(ReviewRequestModel model)
    {
        int userId = GetUserId();
        await _userService.AddMovieReview(model);

        return RedirectToAction("Review");
    }

    [HttpGet]
    public async Task Profile()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task EditProfile()
    {
        throw new NotImplementedException();
    }
    
}