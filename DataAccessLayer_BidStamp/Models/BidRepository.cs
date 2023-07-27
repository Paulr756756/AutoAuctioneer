using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_BidStamp.Models;

public interface IBidRepository {
    Task<List<Bid>> getAllBids();
    Task<List<Bid>> getBidsPerListing(Guid guid);
    Task<Bid> getBidById(Guid id);
    Task postBid(Bid bid);
    Task deleteBid(Bid bid);
    Task updateBidAmt(Bid request);
}

public class BidRepository : IBidRepository {
    private readonly DatabaseContext _dbContext;

    public BidRepository(DatabaseContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<List<Bid>> getAllBids() {
        return await _dbContext.Bids.ToListAsync();
    }

    public async Task<List<Bid>> getBidsPerListing(Guid guid) {
        //TODO() find a more efficient operation
        return await _dbContext.Bids.Where(b => b.ListingId == guid).ToListAsync();
    }

    public async Task<Bid> getBidById(Guid id) {
        return await _dbContext.Bids.FirstOrDefaultAsync(b => b.BidId == id);
    }

    public async Task deleteBid(Bid bid) {
        _dbContext.Bids.Remove(bid);
        await _dbContext.SaveChangesAsync();
    }

    public async Task postBid(Bid bid) {
        await _dbContext.Bids.AddAsync(bid);
        await _dbContext.SaveChangesAsync();
    }

    public async Task updateBidAmt(Bid request) {
        //TODO(find a more efficient way)
        /*var bid = await getBidById(request.BidId);
        bid.BidAmount = request.BidAmount;
        bid.BidTime = request.BidTime;*/
        await _dbContext.SaveChangesAsync();
    }
}