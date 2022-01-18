using System.Text;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class UserReviewResponseModel
{
    public int UserId;
    public List<ReviewRequestModel> Reviews;

    public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.UserId + "\n");

        foreach (var review in this.Reviews)
        {
            sb.Append(review.ToString() + "\n");
        }

        return sb.ToString();
    }

    public static UserReviewResponseModel FromEntity(User user, List<Review> reviews)
    {
        var reviewModels = new List<ReviewRequestModel>();
        foreach (var review in reviews)
        {
            reviewModels.Add(ReviewRequestModel.FromEntity(review));
        }

        return new UserReviewResponseModel { UserId = user.Id, Reviews = reviewModels};
    }
}