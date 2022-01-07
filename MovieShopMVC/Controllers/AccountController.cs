using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}