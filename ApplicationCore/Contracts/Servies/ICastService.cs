using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface ICastService
{
    Task<CastDetailsResponseModel> GetCastDetails(int id);
    
}