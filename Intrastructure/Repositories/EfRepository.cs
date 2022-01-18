using System.Linq.Expressions;
using ApplicationCore.Contracts.Repositories;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Intrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T: class
{
    protected readonly MovieShopDbContext _dbContext;

    public EfRepository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async  Task<T> Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity; 
    }

    public virtual Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        var data = await _dbContext.Set<T>().ToListAsync();
        return data;
    }

    public virtual async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> filter)
    {
        var data = await _dbContext.Set<T>().Where(filter).ToListAsync();
        return data;
    }
    
    public virtual async Task<T> GetById(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        return entity;
    }

    public virtual Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }
}