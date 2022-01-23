using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class ReviewRequestModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal Rating { get; set; }
    public string? ReviewText { get; set; }
    
    public string ToString()
    {
        return this.UserId + " " + this.MovieId + " " + this.Rating + " " + this.ReviewText;
    }

    public static ReviewRequestModel FromEntity(Review review)
    {
        var reviewModel = new ReviewRequestModel
        {
            MovieId = review.MovieId,
            UserId = review.UserId,
            Rating = review.Rating,
            ReviewText = review.ReviewText
        };

        return reviewModel;
    }
}