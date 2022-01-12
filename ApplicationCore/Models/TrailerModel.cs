namespace ApplicationCore.Models;

public class TrailerModel
{
    public int Id { get; set; }
    
    public int MovieId { get; set; }
    
    public string Name { get; set; }
    
    public string TrailerUrl { get; set; }
    
}