using System.Text;
using ApplicationCore.Entities;
using Microsoft.VisualBasic.CompilerServices;

namespace ApplicationCore.Models;

public class CastDetailsResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string TmdbUrl { get; set; }
    public string ProfilePath { get; set; }
    
    // IEumerable
    public List<MovieCardResponseModel> MoviesOfCast { get; set; }

    public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Id + " " + this.Name + " " + this.Gender + " " + this.TmdbUrl + " " + this.ProfilePath + "\n");
        foreach (var card in this.MoviesOfCast)
        {
            sb.Append(card.ToString() + "\n");
        }

        return sb.ToString();
    }

    public static CastDetailsResponseModel FromEntity(Cast cast, List<Movie> movies)
    {
        List<MovieCardResponseModel> movieCards = new List<MovieCardResponseModel>();
        foreach (Movie movie in movies)
        {
            movieCards.Add(MovieCardResponseModel.FromEntity(movie));
        }

        CastDetailsResponseModel castDetails = new CastDetailsResponseModel
        {
            Id = cast.Id,
            Name = cast.Name,
            Gender = cast.Gender,
            TmdbUrl = cast.TmdbUrl,
            ProfilePath = cast.ProfilePath,
            MoviesOfCast = movieCards
        };

        return castDetails;
    }
}