using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IEngineRepository
{
    
}

public class EngineRepository : BaseRepository<Engine>, IEngineRepository
{
    private readonly DatabaseContext _dbContext;

    public EngineRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Engine>> GetEngineByIdAsync(Guid guid)
    {
        return await GetSingleItemAsync(e => e.CarpartId == guid);
    }

    public async Task<Result<Engine>> GetAllEnginesAsync(Guid guid)
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<Engine>> GetEngineOfSingleUserAsync(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.UserId == guid);
    }

    public async Task<Result<Engine>> StoreEngineAsync(Engine engine)
    {
        return await StoreItemAsync(engine);
    }

    public async Task<Result<Engine>> DeleteEngineAsync(Engine engine)
    {
        return await DeleteItemAsync(engine);
    }
}