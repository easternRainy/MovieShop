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
        var movies = await _dbContext.Movies
            .OrderByDescending(m => m.Revenue)
            .Take(30)
            .ToListAsync();
        
        return movies;
    }

    public override async Task<Movie> GetById(int id)
    {
        var movie = await _dbContext.Movies
            .Include(m => m.Trailers)
            .Include(m => m.GenresOfMovie)
            .ThenInclude( m => m.Genre )
            .SingleOrDefaultAsync(m => m.Id == id);
        
        return movie;
    }
    
    public async Task<List<Cast>> GetCastsByMovie(int movieId)
    {
        var casts = await _dbContext.MovieCasts
            .Include(mc => mc.Cast)
            .Where(mc => mc.MovieId == movieId)
            .Select(mc => mc.Cast)
            .OrderBy(c => c.Id)
            .Take(10)
            .ToListAsync();

        return casts;
    }

    public async Task<List<MovieCast>> GetMovieCastsByMovie(int movieId)
    {
        var movieCasts = await _dbContext.MovieCasts
            .Where(mc => mc.MovieId == movieId)
            .OrderBy(mc => mc.CastId)
            .Take(10)
            .ToListAsync();

        return movieCasts;
    }

    

    public async Task<List<Genre>> GetGenresOfMovie(int movieId)
    {
        var genres = await _dbContext.MovieGenres
            .Include(mg => mg.Genre)
            .Where(mg => mg.MovieId == movieId)
            .Select(mg => mg.Genre)
            .Take(10)
            .ToListAsync();

        return genres;
    }

    public async Task<List<Movie>> GetMoviesOfGenre(int genreId)
    {
        var movies = await _dbContext.MovieGenres
            .Include(mg => mg.Movie)
            .Where(mg => mg.GenreId == genreId)
            .Select(mg => mg.Movie)
            .Take(10)
            .ToListAsync();

        return movies;
    }

    public async Task<List<Review>> GetReviewsOfMovie(int movieId)
    {
        var reviews = await _dbContext.Reviews
            .Where(r => r.MovieId == movieId)
            .Take(10)
            .ToListAsync();

        return reviews;
    }

    public async Task<List<Trailer>> GetTrailersOfMovie(int movieId)
    {
        var trailers = await _dbContext.Trailers
            .Where(t => t.MovieId == movieId)
            .Take(10)
            .ToListAsync();

        return trailers;
    }

}