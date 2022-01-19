using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static GenreModel FromEntity(Genre genre)
    {
        return new GenreModel {Id = genre.Id, Name = genre.Name};
    }
}