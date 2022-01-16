using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Intrastructure.Repositories;

public class MovieRepository : EfRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }
    
    // get 30 highest 
    //...
    public async Task<List<Movie>> Get30HighestGrossingMovies()
    {
        var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }

    public override async Task<Movie> GetById(int id)
    {
        var movie = await _dbContext.Movies.Include(m => m.Trailers).Include(m => m.GenresOfMovie).ThenInclude( m => m.Genre ).SingleOrDefaultAsync(m => m.Id == id);
        return movie;
    }

}