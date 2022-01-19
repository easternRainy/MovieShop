using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class TrailerModel
{
    public int Id { get; set; }
    
    public int MovieId { get; set; }
    
    public string Name { get; set; }
    
    public string TrailerUrl { get; set; }

    public string ToString()
    {
        return this.MovieId + " " + this.Name + " " + this.TrailerUrl;
    }

    public static TrailerModel FromEntity(Trailer trailer)
    {
        return new TrailerModel
        {
            Id = trailer.Id,
            MovieId = trailer.MovieId,
            Name = trailer.Name,
            TrailerUrl = trailer.TrailerUrl
        };
    }
    
}