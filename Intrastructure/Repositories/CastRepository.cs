using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
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
    public async Task<Cast> GetById(int id)
    {
        var cast = await _dbContext.Casts.SingleOrDefaultAsync(c => c.Id == id);
        return cast;
    }
    
    public async Task<List<int>> GetMovieIdsById(int id)
    {
        var allMovieIdsByCast = await _dbContext.MovieCasts
            .Where(mc => mc.CastId == id)
            .Select(mc => mc.MovieId)
            .ToListAsync();
            
        return allMovieIdsByCast;
    }

    public async Task<List<Movie>> GetMoviesById(int id)
    {
        var allMoviesByCast = await _dbContext.MovieCasts
            .Include(mc => mc.Movie)
            .Where(mc => mc.CastId == id)
            .Select(mc => mc.Movie)
            .ToListAsync();

        return allMoviesByCast;
    }
    
    public async Task<List<Cast>> GetCastsByMovie(int movieId)
    {
        var casts = await _dbContext.MovieCasts
            .Include(mc => mc.Cast)
            .Where(mc => mc.MovieId == movieId)
            .Select(mc => mc.Cast)
            .OrderBy(c => c.Id)
            .ToListAsync();

        return casts;
    }

    public async Task<List<MovieCast>> GetMovieCastsByMovie(int movieId)
    {
        var movieCasts = await _dbContext.MovieCasts
            .Where(mc => mc.MovieId == movieId)
            .OrderBy(mc => mc.CastId)
            .ToListAsync();

        return movieCasts;
    }
    
    
}


