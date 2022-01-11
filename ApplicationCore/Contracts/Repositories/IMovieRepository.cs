using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public class IMovieRepository: IRepository<Movie>
{
    Task<List<Movie>> Get30HighestGrossingMovies();
}