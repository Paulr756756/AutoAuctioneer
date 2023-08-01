using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface ICarPartRepository
{
    
}
public class CarPartRepository : BaseRepository<CarPart>,ICarPartRepository
{
    private readonly DatabaseContext _dbContext;
    public CarPartRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<CarPart>> GetCarByIdAsync(Guid guid)
    {
        return await GetSingleItemAsync(e => e.CarpartId == guid);
    }

    public async Task<Result<CarPart>> GetAllCarsAsync(Guid guid)
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<CarPart>> GetCarOfSingleUserAsync(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.UserId == guid);
    }

    public async Task<Result<CarPart>> StoreCarAsync(CarPart car)
    {
        return await StoreItemAsync(car);
    }

    public async Task<Result<CarPart>> DeleteCarAsync(CarPart car)
    {
        return await DeleteItemAsync(car);
    }
}