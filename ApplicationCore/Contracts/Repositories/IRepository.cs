using System.Linq.Expressions;

namespace ApplicationCore.Contracts.Repositories;

public interface IRepository<T> where T: class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> filter);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(int id);
}