using System.Linq.Expressions;
using ApplicationCore.Contracts.Repositories;
using Intrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Intrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T: class
{
    protected readonly MovieShopDbContext _dbContext;

    public virtual Task<T> GetById(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync();
        return entity;
    }

    public virtual Task<IEnumerable<T>> GetAll()
    {
        var data = await _dbContext.Set<T>().ToListAsync();
        return data;
    }

    public virtual Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> filter)
    {
        var data = await _dbContext.Set<T>.Where(filter).ToListAsync();
        return data;
    }

}