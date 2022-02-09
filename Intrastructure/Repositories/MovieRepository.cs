using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

        // var query = _dbContext.Movies
        //     .OrderByDescending(m => m.Revenue)
        //     .Take(30);
        // var queryStr = query.ToQueryString();
        
        return movies;
    }

    public async Task<List<Movie>> GetTop30RatingMovies()
    {
        var ids = await _dbContext.Reviews
            .Include(r => r.Movie)
            .GroupBy(r => r.MovieId)
            .Select(g => new
            {
                MovieId = g.Key,
                AvgRate = g.Average(r => r.Rating)
            })
            .OrderByDescending(rg => rg.AvgRate)
            .Select(rg => rg.MovieId)
            .Take(30)
            .ToListAsync();

        // reference: 
        // https://stackoverflow.com/questions/15275269/sort-a-list-from-another-list-ids
        var movies = await _dbContext.Movies
            .Where(m => ids.Contains(m.Id))
            // .OrderBy(m => ids.IndexOf(m.Id)) // does not work
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

    public async Task<PagedResultSet<Movie>> GetMoviesOfGenreByPagination(int genreId, int pageSize=30, int page=1)
    {
        var movies = await _dbContext.MovieGenres
            .Include(mg => mg.Movie)
            .Where(mg => mg.GenreId == genreId)
            .Select(mg => mg.Movie)
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalMoviesCount = await _dbContext.Movies.CountAsync();
        
        return new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCount);
    }

    public async Task<PagedResultSet<Review>> GetReviewsOfMovieByPagination(int movieId, int pageSize=30, int page=1)
    {
        var reviews = await _dbContext.Reviews
            .Where(r => r.MovieId == movieId)
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalReviewsCount = await _dbContext.Reviews.CountAsync();

        return new PagedResultSet<Review>(reviews, page, pageSize, totalReviewsCount);
    }

    public async Task<List<Trailer>> GetTrailersOfMovie(int movieId)
    {
        var trailers = await _dbContext.Trailers
            .Where(t => t.MovieId == movieId)
            .Take(10)
            .ToListAsync();

        return trailers;
    }

    public async Task<PagedResultSet<Movie>> GetMoviesByTitle(int pageSize = 30, int page = 1, string title = "")
    {
        // SELECT * FROM [Movies] WHERE Title LIKE '%ave%' ORDER BY TITLE OFFSET 0 FETCH NEXT ROWS 30;
        var movies = await _dbContext.Movies
            .Where(m => m.Title.Contains(title))
            .OrderBy(m => m.Title)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync(); 
        
        // total movies for that condition
        // SELECT COUNT(*) From Movies WHERE Title LIKE ..
        var totalMoviesCount = await _dbContext.Movies
            .Where(m => m.Title.Contains(title))
            .CountAsync();

        var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCount);

        return pagedMovies;
    }

    public async Task<PagedResultSet<Movie>> GetTopPurchasedMovies(DateTime fromDate, DateTime endDate, int pageSize = 30, int page = 1)
    {
        var movieIds = await _dbContext.Purchases
            .Where(p => p.PurchaseDateTime >= fromDate && p.PurchaseDateTime <= endDate)
            .GroupBy(p => p.MovieId)
            .Select(p => new
            {
                MovieId = p.Key,
                PurchaseCount = p.Count()
            })
            .OrderByDescending(pg => pg.PurchaseCount)
            .Select(pg => pg.MovieId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var movies = await _dbContext.Movies
            .Where(m => movieIds.Contains(m.Id))
            .ToListAsync();
        
        var totalMoviesCount = await _dbContext.Movies.CountAsync();

        var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCount);

        return pagedMovies;
    }

    public async Task CreateMovie(MovieCreateRequestModel model)
    {
        var movieEntity = MovieCreateRequestModel.ToEntity(model);
        await _dbContext.Movies.AddAsync(movieEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateMovieDetails(MovieCreateRequestModel model)
    {
        var movie = await _dbContext.Movies
            .Where(m => m.Title == model.Title)
            .SingleOrDefaultAsync();

        if (movie == null)
        {
            return false;
        }

        List<Genre> genres = new List<Genre>();

        foreach (var genreModel in model.Genres)
        {
            genres.Add(GenreModel.ToEntity(genreModel));
        }

        movie.Overview = model.Overview;
        movie.Tagline = model.Tagline;
        movie.Revenue = model.Revenue;
        movie.Budget = model.Budget;
        movie.ImdbUrl = model.ImdbUrl;
        movie.TmdbUrl = model.TmdbUrl;
        movie.PosterUrl = model.PosterUrl;
        movie.BackdropUrl = model.BackdropUrl;
        movie.OriginalLanguage = model.OriginalLanguage;
        movie.ReleaseDate = model.ReleaseDate;
        movie.RunTime = model.RunTime;
        movie.Price = model.Price;

        await _dbContext.SaveChangesAsync();
        return true;
    }

}