using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class MovieCardResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterUrl { get; set; }
    
    public DateTime ReleaseDate { get; set; }

    public string ToString()
    {
        return this.Id + " " + this.Title + " " + this.PosterUrl;
    }

    public static MovieCardResponseModel FromEntity(Movie movie)
    {
        MovieCardResponseModel movieCard = new MovieCardResponseModel
        {
            Id = movie.Id,
            Title = movie.Title,
            PosterUrl = movie.PosterUrl
        };

        return movieCard;
    }
}