using System.Linq.Expressions;

namespace SupCountBE.Core.Repositories;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    Task<int> CountAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
}
