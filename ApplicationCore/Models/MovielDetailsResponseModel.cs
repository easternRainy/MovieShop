using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class MovieDetailsResponseModel
{
    public MovieDetailsResponseModel()
    {
        Casts = new List<CastModel>();
        Genres = new List<GenreModel>();
        Reviews = new List<UserReviewResponseModel>();
        Trailers = new List<TrailerModel>();
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterUrl { get; set; }
    public string BackdropUrl { get; set; }
    public decimal? Rating { get; set; }
    public string Overview { get; set; }
    public string? Tagline { get; set; }
    public decimal? Budget { get; set; }
    public decimal? Revenue { get; set; }
    public string? ImdbUrl { get; set; }
    public string TmdbUrl { get; set; }
    public DateTime? Releasedate { get; set; }
    public int? RunTime { get; set; }
    public decimal? Price { get; set; }
    public int FavoritesCount { get; set; }
    
    public List<CastModel> Casts { get; set; }
    public List<GenreModel> Genres { get; set; }
    public List<UserReviewResponseModel> Reviews { get; set; }
    public List<TrailerModel> Trailers { get; set; }
    

    public string ToString()
    {
        return "Title: " + this.Title + ' ' + "Rating: " + this.Rating + ' ' + "Budget: " + this.Budget;
    }

    public static MovieDetailsResponseModel FromEntity(Movie movie,
        List<Cast> casts, List<MovieCast> movieCasts, List<Genre> genres, List<Review> reviews, List<Trailer> trailers)
    {
        var castModels = new List<CastModel>();
        var genreModels = new List<GenreModel>();
        var reviewModels = new List<UserReviewResponseModel>();
        var trailerModels = new List<TrailerModel>();

        for (int i = 0; i < casts.Count; i++)
        {
            castModels.Add(CastModel.FromEntity(casts[i], movieCasts[i]));
        }
        

        foreach (var genre in genres)
        {
            genreModels.Add(GenreModel.FromEntity(genre));
        }

        foreach (var trailer in trailers)
        {
            trailerModels.Add(TrailerModel.FromEntity(trailer));
        }



        return new MovieDetailsResponseModel
        {
            Id = movie.Id,
            Title = movie.Title,
            PosterUrl = movie.PosterUrl,
            BackdropUrl = movie.BackdropUrl,
            Rating = 0, // process later
            Overview = movie.Overview,
            Tagline = movie.Tagline,
            Budget = movie.Budget,
            Revenue = movie.Revenue,
            ImdbUrl = movie.ImdbUrl,
            TmdbUrl = movie.TmdbUrl,
            Releasedate = movie.ReleaseDate,
            RunTime = movie.RunTime,
            Price = movie.Price,
            FavoritesCount = 0, // process later
            Casts = castModels,
            Genres = genreModels,
            Reviews = reviewModels,
            Trailers = trailerModels
        };
    }
} 