using System.Text;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class FavoriteResponseModel
{
    public int UserId { get; set; }
    public List<MovieCardResponseModel> MovieCards { get; set; }

    public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.UserId + "\n");
        foreach (var movie in this.MovieCards)
        {
            sb.Append(movie.ToString() + "\n");
        }

        return sb.ToString();
    }

    public static FavoriteResponseModel FromEntity(User user, List<Movie> movies)
    {
        var cards = new List<MovieCardResponseModel>();
        foreach (var movie in movies)
        {
            cards.Add(MovieCardResponseModel.FromEntity(movie));
        }

        return new FavoriteResponseModel {UserId = user.Id, MovieCards = cards};
    }
}