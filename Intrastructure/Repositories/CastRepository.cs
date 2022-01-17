using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace Intrastructure.Repositories;

public class CastRepository: EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }
    
    
    // not tested yet
    // Question: what is the return type if we want to join many models
    public async Task<List<int>> GetMovieIdsById(int id)
    {
        var allMovieIdsByCast = await _dbContext.MovieCasts
            .Where(mc => mc.CastId == id)
            .Select(mc => mc.MovieId)
            .ToListAsync();
            
        return allMovieIdsByCast;
    }
}


