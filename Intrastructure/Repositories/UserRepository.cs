using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Intrastructure.Repositories;

public class UserRepository: EfRepository<User>, IUserRepository
{
    public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }

    /*
     * Account Related Operations
     */
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        return user;
    }
    
    /*
     * Purchase Related Operations
     */

    public async Task<List<Purchase>> GetAllPurchasesOfUser(int id)
    {
        var purchases = await _dbContext.Purchases
            .Where(p => p.UserId == id)
            .ToListAsync();

        return purchases;
    }

    public async Task<Purchase> GetPurchaseByUserAndMovie(int userId, int movieId)
    {
        var purchase = await _dbContext.Purchases
            .Where(p => p.UserId == userId && p.MovieId == movieId)
            .SingleOrDefaultAsync();

        return purchase;
    }

    public async Task<List<Movie>> GetAllMoviesPurchasedByUser(int userId)
    {
        var movies = await _dbContext.Purchases
            .Include(p => p.Movie)
            .Where(p => p.UserId == userId)
            .Select(p => p.Movie)
            .ToListAsync();
        return movies;
    }

    // problem: how to generate unique purchase number
    public async Task<Purchase> AddNewPurchase(int userId, int movieId, decimal price)
    {
        var purchase = await GetPurchaseByUserAndMovie(userId, movieId);

        if (purchase == null)
        {
            // user has already purchased this moive
            Purchase newPurchase = new Purchase
            {
                UserId = userId,
                MovieId = movieId,
                TotalPrice = price,
                PurchaseDateTime = DateTime.Now
            };
            await _dbContext.Purchases.AddAsync(newPurchase);
            await _dbContext.SaveChangesAsync();
            
            return newPurchase;
        }

        return null;
    }
    
    /*
     * Favorite Related Operations
     */
    public async Task<List<Favorite>> GetAllFavoritesOfUser(int id)
    {
        var favorites = await _dbContext.Favorites
            .Where(f => f.UserId == id)
            .ToListAsync();

        return favorites;
    }

    public async Task<Favorite> GetFavoriteByUserAndMovie(int userId, int movieId)
    {
        var purchase = await _dbContext.Favorites
            .Where(f => f.UserId == userId && f.MovieId == movieId)
            .SingleOrDefaultAsync();

        return purchase;
    }

    public async Task<List<Movie>> GetAllMoviesFavoritedByUser(int userId)
    {
        var movies = await _dbContext.Favorites
            .Include(f => f.Movie)
            .Where(f => f.UserId == userId)
            .Select(f => f.Movie)
            .ToListAsync();
        return movies;
    }
    
    /*
     * Review Related Operations
     */
    public async Task<List<Review>> GetAllReviewsOfUser(int id)
    {
        var reviews = await _dbContext.Reviews
            .Where(r => r.UserId == id)
            .ToListAsync();

        return reviews;
    }
    
}

