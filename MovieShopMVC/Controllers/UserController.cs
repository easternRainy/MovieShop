using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}