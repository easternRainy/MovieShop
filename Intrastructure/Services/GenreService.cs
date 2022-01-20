using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class GenreService: IGenreService
{
    private readonly IRepository<Genre> _genreRepository;

    public GenreService(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<List<GenreModel>> GetAllGenres()
    {
        var genres = await _genreRepository.GetAll();
        var genreModels = new List<GenreModel>();
        foreach (var genre in genres)
        {
            genreModels.Add(GenreModel.FromEntity(genre));
        }

        return genreModels;
    }

    public async Task<List<MovieCardResponseModel>> GetMoviesByGenre(int genreId)
    {
        throw new NotImplementedException();
    }
}