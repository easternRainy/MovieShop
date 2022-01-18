using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmail(string email);
    Task<List<Purchase>> GetAllPurchasesOfUser(int id);
    Task<Purchase> GetPurchaseByUserAndMovie(int userId, int movieId);
    Task<List<Movie>> GetAllMoviesPurchasedByUser(int userId);
    Task<Purchase> AddNewPurchase(int userId, int movieId, decimal price);
    Task<List<Favorite>> GetAllFavoritesOfUser(int id);
    Task<Favorite> GetFavoriteByUserAndMovie(int userId, int movieId);
    Task<List<Movie>> GetAllMoviesFavoritedByUser(int userId);
    Task<Favorite> AddNewFavorite(int userId, int movieId);
    Task RemoveFavorite(int userId, int movieId);
    Task<List<Review>> GetAllReviewsOfUser(int id);
}