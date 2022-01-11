using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;

namespace Intrastructure.Repositories;

public class MovieRepository: EfRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext)
}