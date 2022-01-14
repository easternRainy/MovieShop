using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AccountController : Controller
{
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterRequestModel model)
    {
        // save the data in the User Table
        return View();
    }
}

