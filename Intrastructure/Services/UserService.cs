using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService: IUserService
{
    public readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
    {
        //List<Purchase> purchases = await _userRepository.GetAllPurchasesOfUser(id);
        throw new NotImplementedException();
        
    }

    public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
    {
        throw new NotImplementedException();
    }

    public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> FavoriteExists(int id, int movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddMovieReview(ReviewRequestModel reviewRequest)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMovieReview(int userId, int movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
    {
        throw new NotImplementedException();
    }

}