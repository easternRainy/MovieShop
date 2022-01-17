using ApplicationCore.Entities;
using Microsoft.VisualBasic.CompilerServices;

namespace ApplicationCore.Contracts.Repositories;

public interface ICastRepository: IRepository<Cast>
{
    Task<List<int>> GetMovieIdsById(int id);
}