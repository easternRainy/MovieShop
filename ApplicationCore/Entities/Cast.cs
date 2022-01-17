namespace ApplicationCore.Entities;

public class Cast
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Gender { get; set; }
    public string? TmdbUrl { get; set; }
    public string? ProfilePath { get; set; }

    public string ToString()
    {
        return this.Id + " " + this.Name + " " + this.Gender + " " + this.TmdbUrl + " " + this.ProfilePath;
    }
}