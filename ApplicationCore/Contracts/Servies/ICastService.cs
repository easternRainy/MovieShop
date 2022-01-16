namespace ApplicationCore.Contracts.Servies;

public interface ICastService
{
    Task<CastDetailsReponseModel> GetCastDetails(int id);
}