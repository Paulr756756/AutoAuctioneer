using System.Linq.Expressions;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<Result<TEntity>> GetALlItemsAsync();
    Task<Result<TEntity>> GetSingleItemAsync<T>(Expression<Func<TEntity, bool>> predicate);
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
    public async Task<Result<TEntity>> GetALlItemsAsync()
    {
        try
        {
            var response = await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            return Result<TEntity>.SuccessList(response);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TEntity>.Failure(e.ToString());
            /*throw;*/
        }
    }

    public async Task<Result<TEntity>> GetSingleItemAsync<T>(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var response = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return Result<TEntity>.Success(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TEntity>.Failure(e.ToString());
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