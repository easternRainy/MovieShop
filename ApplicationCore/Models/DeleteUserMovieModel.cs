using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class DeleteUserMovieModel
{
    public int UserId { get; set; }
    public int MovieId { get; set; }

    public string ToString()
    {
        return this.UserId + " " + this.MovieId;
    }

    public static DeleteUserMovieModel FromEntity(User user, Movie movie)
    {
        return new DeleteUserMovieModel {UserId = user.Id, MovieId = movie.Id};
    }

}