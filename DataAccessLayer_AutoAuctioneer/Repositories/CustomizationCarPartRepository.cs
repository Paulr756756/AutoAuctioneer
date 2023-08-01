using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface ICustomizationCarPartRepository
{
    Task<Result<CustomizationPart>> GetCustomizationPartByIdAsync(Guid guid);
    Task<Result<CustomizationPart>> GetAllCustomizationPartsAsync(Guid guid);
    Task<Result<CustomizationPart>> GetCustomizationPartOfSingleUserAsync(Guid guid);
    Task<Result<CustomizationPart>> StoreCustomizationPartAsync(CustomizationPart part);
    Task<Result<CustomizationPart>> DeleteCustomizationPartAsync(CustomizationPart part);
}

public class CustomizationCarPartRepository : BaseRepository<CustomizationPart>, ICustomizationCarPartRepository
{
    private readonly DatabaseContext _dbContext;

    public CustomizationCarPartRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CustomizationPart>> GetCustomizationPartByIdAsync(Guid guid)
    {
        return await GetSingleItemAsync(e => e.CarpartId == guid);
    }

    public async Task<Result<CustomizationPart>> GetAllCustomizationPartsAsync(Guid guid)
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<CustomizationPart>> GetCustomizationPartOfSingleUserAsync(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.UserId == guid);
    }

    public async Task<Result<CustomizationPart>> StoreCustomizationPartAsync(CustomizationPart part)
    {
        return await StoreItemAsync(part);
    }

    public async Task<Result<CustomizationPart>> DeleteCustomizationPartAsync(CustomizationPart part)
    {
        return await DeleteItemAsync(part);
    }
}