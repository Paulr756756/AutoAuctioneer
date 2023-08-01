using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IListingRepository : IBaseRepository<Listing>
{
    Task<Result<Listing>> GetAllListings();
    Task<Result<Listing>> GetListingById(Guid guid);
    Task<Result<Listing>> PostListing(Listing listing);
    Task<Result<Listing>> DeleteListing(Listing listing);
}

public class ListingRepository : BaseRepository<Listing>,IListingRepository
{
    private readonly DatabaseContext _dbContext;

    public ListingRepository(DatabaseContext databaseContext):base(databaseContext)
    {
        _dbContext = databaseContext;
    }

    public async Task<Result<Listing>> GetAllListings()
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<Listing>> GetListingById(Guid guid)
    {
        return await GetSingleItemAsync(e => e.ListingId == guid);
    }

    public async Task<Result<Listing>> PostListing(Listing listing)
    {
        return await StoreItemAsync(listing);
    }

    public async Task<Result<Listing>> DeleteListing(Listing listing)
    {
        return await DeleteItemAsync(listing);
    }
}