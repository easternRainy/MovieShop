using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

[Authorize]
public class UserController : Controller
{
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }

    
    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        // call user service with login-ed user id and get the movies user purchased from Purchase table
        var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        
    }
}