using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository: IRepository<Movie>
{
    Task<List<Movie>> Get30HighestGrossingMovies();
    Task<List<Cast>> GetCastsByMovie(int movieId);
    Task<List<MovieCast>> GetMovieCastsByMovie(int movieId);
    Task<List<Genre>> GetGenresOfMovie(int movieId);
    Task<List<Movie>> GetMoviesOfGenre(int genreId);
    Task<List<Review>> GetReviewsOfMovie(int movieId);
    Task<List<Trailer>> GetTrailersOfMovie(int movieId);
}