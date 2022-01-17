using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService: ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<CastDetailsResponseModel> GetCastDetails(int id)
    {
        Cast cast = await _castRepository.GetById(id);
        List<MovieCardResponseModel> moviesOfCast = await _castRepository.GetMoviesById(id);

        CastDetailsResponseModel castDetails = new CastDetailsResponseModel
        {
            Id = id,
            Name = cast.Name,
            Gender = cast.Gender,
            TmdbUrl = cast.TmdbUrl,
            ProfilePath = cast.ProfilePath,
            MoviesOfCast = moviesOfCast
        };

        return castDetails;
    }
}