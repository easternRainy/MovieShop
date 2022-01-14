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
    
    // public override async Task<Cast> GetById(int id)
    // {
    //     var cast = await _dbContext.Casts.Include(m => m.Trailers).Include(m => m.GenresOfMovie).ThenInclude( m => m.Genre ).SingleOrDefaultAsync(m => m.Id == id);
    //     return cast;
    // }
}


