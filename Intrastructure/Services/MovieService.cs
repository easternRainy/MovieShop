using System.Xml.Schema;
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

    public async Task<List<MovieCardResponseModel>> GetTop30RatingMovies()
    {
        var movies = await _movieRepository.GetTop30RatingMovies();
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
        //var reviews = await _movieRepository.GetReviewsOfMovieByPagination(id);
        var trailers = await _movieRepository.GetTrailersOfMovie(id);
        var movieDetails = MovieDetailsResponseModel.FromEntity(movie, casts, movieCasts, genres, null, trailers);

        return movieDetails;
    }

    public async Task<PagedResultSet<MovieCardResponseModel>> GetMoviesOfGenreByPagination(int id, int pageSize=30, int page=1)
    {
        int genreId = id;

        var pagedMovies = await _movieRepository.GetMoviesOfGenreByPagination(genreId, pageSize, page);
        var pagedMovieCards = new List<MovieCardResponseModel>();
        
        pagedMovieCards.AddRange(pagedMovies.Data.Select(m => MovieCardResponseModel.FromEntity(m)));

        return new PagedResultSet<MovieCardResponseModel>(pagedMovieCards, page, pageSize, pagedMovies.Count);
    }

    public async Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByPagination(int pageSize, int page,
        string title)
    {
        var pagedMovies = await _movieRepository.GetMoviesByTitle(pageSize, page, title);
        var pagedMovieCards = new List<MovieCardResponseModel>();

        pagedMovieCards.AddRange(pagedMovies.Data.Select(
            m => new MovieCardResponseModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl
            }));

        return new PagedResultSet<MovieCardResponseModel>(pagedMovieCards, page, pageSize, pagedMovies.Count);
    }

    public async Task<PagedResultSet<MovieReviewResponseModel>> GetReviewsOfMovieByPagination(int movieId, int pageSize=30, int page=1)
    {
        var pagedReviews = await _movieRepository.GetReviewsOfMovieByPagination(movieId, pageSize, page);
        var pagedReviewModels = new List<MovieReviewResponseModel>();
        
        pagedReviewModels.AddRange(pagedReviews.Data.Select(r => MovieReviewResponseModel.FromEntity(r)));

        return new PagedResultSet<MovieReviewResponseModel>(pagedReviewModels, page, pageSize, pagedReviews.Count);
    }

    public async Task<PagedResultSet<MovieCardResponseModel>> GetTopPurchasedMoviesByPagination(DateTime fromDate, DateTime endDate, int pageSize, int page)
    {
        var pagedMovies = await _movieRepository.GetTopPurchasedMovies(fromDate, endDate, pageSize, page);
        var pagedMovieCards = new List<MovieCardResponseModel>();
        
        pagedMovieCards.AddRange(pagedMovies.Data.Select(
                m => MovieCardResponseModel.FromEntity(m)
            ));

        return new PagedResultSet<MovieCardResponseModel>(pagedMovieCards, page, pageSize, pagedMovies.Count);
    }
    
    public async Task CreateMovie(MovieCreateRequestModel model)
    {
        await _movieRepository.CreateMovie(model);
    }

    public async Task<bool> UpdateMovieDetails(MovieCreateRequestModel model)
    {
        var success = await _movieRepository.UpdateMovieDetails(model);
        return success;
    }

}