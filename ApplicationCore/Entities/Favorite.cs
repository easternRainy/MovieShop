namespace ApplicationCore.Entities;

public class Favorite
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    
    public Movie Movie { get; set; }
    public User User { get; set; }

    public string ToString()
    {
        return this.MovieId + " " + this.UserId;
    }
}