namespace ApplicationCore.Models;

public class MovieCardResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterUrl { get; set; }

    public string ToString()
    {
        return this.Id + " " + this.Title + " " + this.PosterUrl;
    }
}