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
        List<Movie> moviesOfCast = await _castRepository.GetMoviesById(id);
        CastDetailsResponseModel castDetails = CastDetailsResponseModel.FromEntity(cast, moviesOfCast);

        return castDetails;
    }
}