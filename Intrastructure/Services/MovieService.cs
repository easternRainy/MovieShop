using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    public async Task<List<MovieCardResponseModel>> GetTop30GrossingMovies()
    {
        
        var movies = new List<MovieCardResponseModel>()
        {
            new MovieCardResponseModel() {Id = 1, Title = "Inception", PosterUrl = "https://image.tmdb.org/t/p"},
            new MovieCardResponseModel() {Id = 2, Title = "Intersteller", PosterUrl = ""},
            new MovieCardResponseModel() {Id = 3, Title = "The Dark Knight", PosterUrl = ""}
        };

        return movies;
    }
}