using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository: IRepository<Movie>
{
    Task<List<Movie>> Get30HighestGrossingMovies();
    Task<List<Movie>> GetTop30RatingMovies();
    Task<List<Cast>> GetCastsByMovie(int movieId);
    Task<List<MovieCast>> GetMovieCastsByMovie(int movieId);
    Task<List<Genre>> GetGenresOfMovie(int movieId);
    Task<PagedResultSet<Movie>> GetMoviesOfGenreByPagination(int genreId, int pageSize=30, int page=1);
    Task<PagedResultSet<Review>> GetReviewsOfMovieByPagination(int movieId, int pageSize=30, int page=1);
    Task<List<Trailer>> GetTrailersOfMovie(int movieId);
    Task<PagedResultSet<Movie>> GetMoviesByTitle(int pageSize=30, int page=1, string title="");
    Task<PagedResultSet<Movie>> GetTopPurchasedMovies(DateTime fromDate, DateTime endDate, int pageSize = 30, int page = 1);
    Task CreateMovie(MovieCreateRequestModel model);
    Task<bool> UpdateMovieDetails(MovieCreateRequestModel model);
}