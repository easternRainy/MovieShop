using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService: ICastService
{
    private readonly ICastService _castRepository;

    public CastService(ICastService castRepository)
    {
        _castRepository = castRepository;
    }
    
    // Not tested yet
    public async Task<CastDetailsResponseModel> GetCastDetails(int id)
    {
        var castDetails = await _castRepository.GetCastDetails(id);
        var castDetailsModel = new CastDetailsResponseModel
        {
            Id = id,
            Gender = castDetails.Gender,
            Name = castDetails.Name,
            ProfilePath = castDetails.ProfilePath,
            TmdbUrl = castDetails.TmdbUrl
        };

        return castDetailsModel;
    }
}