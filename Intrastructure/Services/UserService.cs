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
        var newPurchase =
            await _userRepository.AddNewPurchase(userId, purchaseRequest.MovieId, purchaseRequest.TotalPrice);

        return newPurchase != null;
    }

    public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        var purchase = await _userRepository.GetPurchaseByUserAndMovie(userId, purchaseRequest.MovieId);
        return purchase != null;
    }

    public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
    {
        
        var user = await _userRepository.GetById(id);
        var movies = await _userRepository.GetAllMoviesPurchasedByUser(id);
        var purchaseModel = PurchaseResponseModel.FromEntity(user, movies);

        return purchaseModel;

    }

    public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
    {
        throw new NotImplementedException();
    }

    public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
    {
        int userId = favoriteRequest.UserId;
        int movieId = favoriteRequest.MovieId;
        var favorite = await _userRepository.AddNewFavorite(userId, movieId);
    }

    public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
    {
        int userId = favoriteRequest.UserId;
        int movieId = favoriteRequest.MovieId;
        await _userRepository.RemoveFavorite(userId, movieId);
    }

    public async Task<bool> FavoriteExists(int id, int movieId)
    {
        Favorite favorite = await _userRepository.GetFavoriteByUserAndMovie(id, movieId);
        return favorite != null;
    }

    // not tested yet
    public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
    {
        var user = await _userRepository.GetById(id);
        var movies = await _userRepository.GetAllMoviesFavoritedByUser(id);
        var favoriteModel = FavoriteResponseModel.FromEntity(user, movies);

        return favoriteModel;
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