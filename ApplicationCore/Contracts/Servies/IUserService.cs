using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface IUserService
{
    Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest);
    Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest);
    
    Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
    
    Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId);

    Task DeletePurchase(PurchaseRequestModel purchaseRequestModel);
    
    Task AddFavorite(FavoriteRequestModel favoriteRequest);

    Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
    
    Task<bool> FavoriteExists(int id, int movieId);
    
    Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
    
    Task AddMovieReview(ReviewRequestModel reviewRequest);
    
    Task UpdateMovieReview(ReviewRequestModel reviewRequest);

    Task PutMovieReview(ReviewRequestModel reviewRequest);

    Task DeleteMovieReview(int userId, int movieId);
    
    Task<UserReviewResponseModel> GetAllReviewsByUser(int id);

}