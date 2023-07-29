using System.Linq.Expressions;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<Result<TEntity>> GetALlItemsAsync();
    Task<Result<TEntity>> GetSingleItemAsync(Expression<Func<TEntity, bool>> predicate);
    Task<Result<TEntity>> StoreItemAsync(TEntity item);
    Task<Result<TEntity>> UpdateItemAsync(TEntity item);
    Task<Result<TEntity>> DeleteItemAsync(TEntity item);
}

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DatabaseContext _dbContext;

    public BaseRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
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

    public async Task<Result<TEntity>> GetSingleItemAsync(Expression<Func<TEntity, bool>> predicate)
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

    public async Task<Result<TEntity>> StoreItemAsync(TEntity item)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TEntity>.Failure(e.ToString());
        }

        return Result<TEntity>.SuccessNoData();
    }

    public async Task<Result<TEntity>> UpdateItemAsync(TEntity item)
    {
        try
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TEntity>.Failure(e.ToString());
        }

        return Result<TEntity>.SuccessNoData();
    }

    public async Task<Result<TEntity>> DeleteItemAsync(TEntity item)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TEntity>.Failure(e.ToString());
        }
        return Result<TEntity>.SuccessNoData();
    }
    
}