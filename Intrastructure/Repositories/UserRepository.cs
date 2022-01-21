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

    public async Task<Favorite> AddNewFavorite(int userId, int movieId)
    {
        var favorite = await GetFavoriteByUserAndMovie(userId, movieId);
        if (favorite == null)
        {
            var newFavorite = new Favorite {UserId = userId, MovieId = movieId};
            await _dbContext.Favorites.AddAsync(newFavorite);
            await _dbContext.SaveChangesAsync();

            return newFavorite;
        }
        return null;
    }

    public async Task<Favorite> RemoveFavorite(int userId, int movieId)
    {
        var favorite = await GetFavoriteByUserAndMovie(userId, movieId);
        if (favorite != null)
        {
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        return null;
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
   
    public async Task<Review> GetReviewByUserAndMovie(int userId, int movieId)
    {
        var review = await _dbContext.Reviews
            .Where(r => r.UserId == userId && r.MovieId == movieId)
            .SingleOrDefaultAsync();

        return review;
    }
    
    public async Task<Review> AddNewReview(int userId, int movieId, decimal rating, string text)
    {
        var review = await GetReviewByUserAndMovie(userId, movieId);

        if (review == null)
        {
            var newReview = new Review { UserId = userId, MovieId = movieId, Rating = rating, ReviewText = text};
            await _dbContext.Reviews.AddAsync(newReview);
            await _dbContext.SaveChangesAsync();
            return review;
        }
        else
        {
            var newReview = await UpdateReview(userId, movieId, rating, text);
            return newReview;
        }
    }
    
    public async Task<Review> UpdateReview(int userId, int movieId, decimal rating, string text)
    {
        var review = await GetReviewByUserAndMovie(userId, movieId);
        
        if (review == null)
        {
            var newReview = await AddNewReview(userId, movieId, rating, text);
            return newReview;
        }
        else
        {

            var query = await _dbContext.Reviews
                .Where(r => r.UserId == userId && r.MovieId == movieId)
                .SingleOrDefaultAsync();

            query.Rating = rating;
            query.ReviewText = text;

            try
            {
                await _dbContext.SaveChangesAsync();
                return new Review {UserId = userId, MovieId = movieId, Rating = rating, ReviewText = text};
            }
            catch
            {
                throw new DbUpdateException();
            }
        }
    }

    public async Task<Review> DeleteReviewByUserAndMovie(int userId, int movieId)
    {
        var review = await GetReviewByUserAndMovie(userId, movieId);
        if (review == null)
        {
            return null;
        }
        else
        {
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
    
}

