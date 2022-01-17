namespace ApplicationCore.Entities;

public class Review
{
    public int MovieId;
    public int UserId;
    public decimal Rating;
    public string? ReviewText;
    
    public Movie Movie { get; set; }
    public User User { get; set; }

}