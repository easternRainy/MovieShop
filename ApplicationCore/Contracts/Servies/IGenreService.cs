using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface IGenreService
{
    Task<List<GenreModel>> GetAllGenres();
}