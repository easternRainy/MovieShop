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

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        return user;
    }

    public async Task<List<Purchase>> GetAllPurchasesOfUser(int id)
    {
        var purchases = await _dbContext.Purchases
            .Where(p => p.UserId == id)
            .ToListAsync();

        return purchases;
    }
}

