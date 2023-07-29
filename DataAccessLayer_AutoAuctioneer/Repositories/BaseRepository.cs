using System.Linq.Expressions;
using DataAccessLibrary_AutoAuctioneer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetALlItemsAsync();
    Task<TEntity> GetSingleItemAsync<T>(Expression<Func<TEntity, bool>> predicate);
    Task UpdateItemAsync<T>(TEntity item);
}

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DatabaseContext _dbContext;

    public BaseRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Why do we use IList instead of a list
    public async Task<List<TEntity>> GetALlItemsAsync()
    {
        try
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TEntity> GetSingleItemAsync<T>(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateItemAsync<T>(TEntity item)
    {
        _dbContext.Entry(item).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteItemAsync<T>(TEntity item)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}