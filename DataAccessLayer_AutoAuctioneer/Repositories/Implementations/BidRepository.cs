/*using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using DataAccessLibrary_AutoAuctioneer.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;
public class BidRepository : BaseRepository, IBidRepository {
    private readonly IConfiguration _config;

    public BidRepository(IConfiguration config) : base(config) {
        _config = config;
    }

    public async Task<List<Bid>?> GetAllBids() {
        throw new NotImplementedException();
    }

    public async Task<List<Bid>> GetBidsPerListing(Guid guid) {
        throw new NotImplementedException();
    }

    public async Task<Bid?> GetBidById(Guid id) {
        throw new NotImplementedException();
    }

    public async Task<bool> PostBid(Bid bid) {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateBidAmt(Bid bid, Guid id) {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBid(Guid id) {
        throw new NotImplementedException();
    }
}*/