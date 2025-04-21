using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using System.Linq.Expressions;

namespace SupCountBE.Infrastacture.Repositories;

public class AsyncRepository<T> : IAsyncRepository<T>    where T : class 
{
    protected readonly SupCountDbContext _dbContext;

    public AsyncRepository(SupCountDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
    {
        var result = _dbContext.Set<T>().Where(expression);
        return Task.FromResult((IReadOnlyList<T>)result.ToList());
    }
    public Task<int> CountAsync()
    {
        var count = _dbContext.Set<T>().Count();
        return Task.FromResult(count);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        var any = _dbContext.Set<T>().Any(expression);
        return Task.FromResult(any);
    }

    public string GetCurrentUser()
    {
        return _dbContext.UserId!;
    }
}
