using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{

    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    public async Task<List<MovieCardResponseModel>> GetTop30GrossingMovies()
    {
        var movies = await _movieRepository.Get30HighestGrossingMovies();
        var movieCards = new List<MovieCardResponseModel>();

        foreach (var movie in movies)
        {
            movieCards.Add(MovieCardResponseModel.FromEntity(movie));
        }

        return movieCards;
    }

    public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
    {
        var movieDetails = await _movieRepository.GetById(id);
        var movieModel = new MovieDetailsResponseModel
        {
            Id = movieDetails.Id,
            Title = movieDetails.Title,
            PosterUrl = movieDetails.PosterUrl,
            BackdropUrl = movieDetails.BackdropUrl,
            ImdbUrl = movieDetails.ImdbUrl
        };

        foreach (var genre in movieDetails.GenresOfMovie)
        {
            movieModel.Genres.Add(new GenreModel {Id = genre.GenreId, Name=genre.Genre.Name});
        }

        foreach (var trailer in movieDetails.Trailers)
        {
            movieModel.Trailers.Add(new TrailerModel {Id = trailer.Id, Name=trailer.Name, TrailerUrl = trailer.TrailerUrl});
        }

        return movieModel;
    }

    
}