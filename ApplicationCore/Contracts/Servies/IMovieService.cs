using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface IMovieService
{
    // create these service methods based on UI/business requirements
    // controller will access theses methods
    Task<List<MovieCardResponseModel>> GetTop30GrossingMovies();
    Task<List<MovieCardResponseModel>> GetTop30RatingMovies();
    Task<MovieDetailsResponseModel> GetMovieDetails(int id);
    Task<PagedResultSet<MovieCardResponseModel>> GetMoviesOfGenreByPagination(int genreId, int pageSize=30, int page=1);
    Task<PagedResultSet<MovieReviewResponseModel>> GetReviewsOfMovieByPagination(int movieId, int pageSize=30, int page=1);
    Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByPagination(int pageSize, int page, string title);
    Task<PagedResultSet<MovieCardResponseModel>> GetTopPurchasedMoviesByPagination(DateTime fromDate, DateTime endDate, int pageSize, int page);
    Task CreateMovie(MovieCreateRequestModel model);

    Task<bool> UpdateMovieDetails(MovieCreateRequestModel model);
}