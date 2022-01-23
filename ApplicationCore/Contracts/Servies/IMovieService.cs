using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface IMovieService
{
    // create these service methods based on UI/business requirements
    // controller will access theses methods
    Task<List<MovieCardResponseModel>> GetTop30GrossingMovies();
    Task<List<MovieCardResponseModel>> GetTop30RatingMovies();
    Task<MovieDetailsResponseModel> GetMovieDetails(int id);
    Task<List<MovieCardResponseModel>> GetMoviesOfGenre(int genreId);
    Task<List<MovieReviewResponseModel>> GetReviewsOfMovie(int movieId);
    Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByPagination(int pageSize, int page, string title);
    Task<PagedResultSet<MovieCardResponseModel>> GetTopPurchasedMoviesByPagination(int pageSize, int page);
}