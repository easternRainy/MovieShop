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
}