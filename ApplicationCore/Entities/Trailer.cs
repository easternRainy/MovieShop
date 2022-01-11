namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string TrailerUrl { get; set; }
    
    // Foreign Keys
    public int MovieId { get; set; }
    
    // migration property
    public Movie Movie { get; set; }
}