namespace ApplicationCore.Contracts.Servies;

public interface IMovieService
{
    // create these service methods based on UI/business requirements
    // controller will access theses methods
    List<MovieCardResponseModel> GetTop30GrossingMovies();
}