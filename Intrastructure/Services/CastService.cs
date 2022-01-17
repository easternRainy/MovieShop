using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
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
        throw new NotImplementedException();
    }
}