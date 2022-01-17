using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace ApplicationCore.Contracts.Repositories;

public interface ICastRepository: IRepository<Cast>
{
    Task<List<int>> GetMovieIdsById(int id);
    Task<List<MovieCardResponseModel>> GetMoviesById(int id);
}