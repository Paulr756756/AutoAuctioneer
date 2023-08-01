using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IIndividualCarPartRepository
{
    Task<Result<IndividualCarPart>> GetIndividualCarPartByIdAsync(Guid guid);
    Task<Result<IndividualCarPart>> GetAllIndividualCarPartsAsync(Guid guid);
    Task<Result<IndividualCarPart>> GetIndividualCarPartOfSingleUserAsync(Guid guid);
    Task<Result<IndividualCarPart>> StoreIndividualCarPartAsync(IndividualCarPart part);
    Task<Result<IndividualCarPart>> DeleteIndividualCarPartAsync(IndividualCarPart part);
}

public class IndividualCarPartRepository : BaseRepository<IndividualCarPart>, IIndividualCarPartRepository
{
    private readonly DatabaseContext _dbContext;

    public IndividualCarPartRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IndividualCarPart>> GetIndividualCarPartByIdAsync(Guid guid)
    {
        return await GetSingleItemAsync(e => e.CarpartId == guid);
    }

    public async Task<Result<IndividualCarPart>> GetAllIndividualCarPartsAsync(Guid guid)
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<IndividualCarPart>> GetIndividualCarPartOfSingleUserAsync(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.UserId == guid);
    }

    public async Task<Result<IndividualCarPart>> StoreIndividualCarPartAsync(IndividualCarPart part)
    {
        return await StoreItemAsync(part);
    }

    public async Task<Result<IndividualCarPart>> DeleteIndividualCarPartAsync(IndividualCarPart part)
    {
        return await DeleteItemAsync(part);
    }
}