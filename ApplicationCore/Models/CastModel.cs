using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class CastModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Character { get; set; }
    public string ProfilePath { get; set; }

    public static CastModel FromEntity(Cast cast, MovieCast movieCast)
    {
        return new CastModel
        {
            Id = cast.Id,
            Name = cast.Name,
            Character = movieCast.Character,
            ProfilePath = cast.ProfilePath
        };
    }
}