namespace Intrastructure.Data;

public class MovieShopDbContexts: DbContext
{
    public MovieShopDbContexts(DbContextOptions<MovieShopDbContext> options): base(options)
    {
        
    }
}