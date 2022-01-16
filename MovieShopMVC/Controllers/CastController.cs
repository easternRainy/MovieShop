using ApplicationCore.Contracts.Servies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class CastController : Controller
{

    private readonly ICastService _castService;
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }

    public CastController(ICastService castService)
    {
        _castService = castService;
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var castDetails = await _castService.GetCastDetails(id);
        return View(castDetails);
    }
}