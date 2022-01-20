using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface IMovieService
{
    // create these service methods based on UI/business requirements
    // controller will access theses methods
    Task<List<MovieCardResponseModel>> GetTop30GrossingMovies();
    Task<MovieDetailsResponseModel> GetMovieDetails(int id);
    Task<List<MovieCardResponseModel>> GetMoviesOfGenre(int genreId);

}