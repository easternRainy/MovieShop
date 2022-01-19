using System.Text;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class PurchaseResponseModel
{
    public int UserId { get; set; }

    public int TotalMoviesPurchased;
    public List<MovieCardResponseModel> MovieCards { get; set; }

    public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.UserId + "\n");
        sb.Append("Total movie purchased: " + this.TotalMoviesPurchased + "\n");
        foreach (var card in MovieCards)
        {
            sb.Append(card.ToString() + "\n");
        }

        return sb.ToString();
    }

    public static PurchaseResponseModel FromEntity(User user, List<Movie> movies)
    {
        var cards = new List<MovieCardResponseModel>();
        foreach(var movie in movies)
        {
            cards.Add(MovieCardResponseModel.FromEntity(movie));
        }

        return new PurchaseResponseModel {UserId = user.Id, MovieCards = cards, TotalMoviesPurchased = cards.Count};
    }

}