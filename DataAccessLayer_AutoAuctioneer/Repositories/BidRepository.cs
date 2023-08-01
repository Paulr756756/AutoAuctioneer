using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IBidRepository : IBaseRepository<Bid>
{
    Task<Result<Bid>> GetAllBids();
    Task<Result<Bid>> GetBidsPerListing(Guid guid);
    Task<Result<Bid>> GetBidById(Guid id);
    Task<Result<Bid>> PostBid(Bid bid);
    Task<Result<Bid>> DeleteBid(Bid bid);
    Task<Result<Bid>> UpdateBidAmt(Bid request);
}

public class BidRepository : BaseRepository<Bid>,IBidRepository
{
    private readonly DatabaseContext _dbContext;

    public BidRepository(DatabaseContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Bid>> GetAllBids()
    {
        return await GetALlItemsAsync();
    }

    public async Task<Result<Bid>> GetBidsPerListing(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.ListingId == guid);
    }

    public async Task<Result<Bid>> GetBidById(Guid id)
    {
        return await GetSingleItemAsync(e=>e.BidId == id);
    }

    public async Task<Result<Bid>> DeleteBid(Bid bid)
    {
        return await DeleteItemAsync(bid);
    }

    public async Task<Result<Bid>> PostBid(Bid bid)
    {
        return await StoreItemAsync(bid);
    }

    public async Task<Result<Bid>> UpdateBidAmt(Bid request)
    {
        return await UpdateItemAsync(request);
    }
}