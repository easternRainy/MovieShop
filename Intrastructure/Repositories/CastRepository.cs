using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Intrastructure.Repositories;

public class CastRepository: EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }
    
    
    // not tested yet
    public async Task<List<Cast>> GetMoviesById(int id)
    {
        
        
        var allMovieIdsByCast = await _dbContext.MovieCasts
                                              .Include(mc => mc.Cast)
                                              .Where(mc => mc.CastId == id)
                                              .Select(mc => new
                                              {
                                                  mc.MovieId, 
                                                  mc.Cast.Name, 
                                                  mc.Character, 
                                                  mc.Cast.Gender, 
                                                  mc.Cast.ProfilePath,
                                                  mc.Cast.TmdbUrl
                                              })
                                              .ToListAsync();
        return allMovieIdsByCast;
    }
}


