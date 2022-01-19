using ApplicationCore.Contracts.Servies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Views.Shared.Components.Genres;

public class GenresViewComponent: ViewComponent
{
    private readonly IGenreService _genreService;

    public GenresViewComponent(IGenreService genreService)
    {
        _genreService = genreService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        // get the list of genres
        var genres = await _genreService.GetAllGenres();
        return View(genres);
    }
}