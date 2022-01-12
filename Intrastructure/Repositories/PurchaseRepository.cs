using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Intrastructure.Data;

namespace Intrastructure.Repositories;

public class PurchaseRepository: EfRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }
}