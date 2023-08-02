using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface ICarPartRepository
{
    Task<Result<CarPart>> GetCarPartById(Guid guid);
    Task<Result<CarPart>> GetAllCarParts();
    Task<Result<CarPart>> GetCarPartOfSingleUser(Guid guid);
    Task<Result<CarPart>> StoreCarPartAsync(CarPart part);
    Task<Result<CarPart>> UpdateCarPart(CarPart part);
    Task<Result<CarPart>> DeleteCarPartAsync(CarPart part);

}
public class CarPartRepository : BaseRepository<CarPart>,ICarPartRepository
{
    private readonly DatabaseContext _dbContext;
    public CarPartRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<CarPart>> GetCarPartById(Guid guid)
    {
        return await GetSingleItemAsync(e => e.CarpartId == guid);
    }

    public async Task<Result<CarPart>> GetAllCarParts()
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<CarPart>> GetCarPartOfSingleUser(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.UserId == guid);
    }

    public async Task<Result<CarPart>> StoreCarPartAsync(CarPart part)
    {
        return await StoreItemAsync(part);
    }
    public async Task<Result<CarPart>> UpdateCarPart(CarPart part)
    {
        return await UpdateItemAsync(part);
    }

    public async Task<Result<CarPart>> DeleteCarPartAsync(CarPart part)
    {
        return await DeleteItemAsync(part);
    }
}