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

        var movie = await _movieRepository.GetById(id);
        var casts = await _movieRepository.GetCastsByMovie(id);
        var movieCasts = await _movieRepository.GetMovieCastsByMovie(id);
        var genres = await _movieRepository.GetGenresOfMovie(id);
        var reviews = await _movieRepository.GetReviewsOfMovie(id);
        var trailers = await _movieRepository.GetTrailersOfMovie(id);
        var movieDetails = MovieDetailsResponseModel.FromEntity(movie, casts, movieCasts, genres, reviews, trailers);

        return movieDetails;
    }

    public async Task<List<MovieCardResponseModel>> GetMoviesOfGenre(int id)
    {
        int genreId = id;
        
        var movies = await _movieRepository.GetMoviesOfGenre(genreId);
        List<MovieCardResponseModel> movieModels = new List<MovieCardResponseModel>();
        foreach (var movie in movies)
        {
            movieModels.Add(MovieCardResponseModel.FromEntity(movie));
        }

        return movieModels;
    }

    
}