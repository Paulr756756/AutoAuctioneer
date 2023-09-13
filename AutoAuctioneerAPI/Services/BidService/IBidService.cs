using API_AutoAuctioneer.Models.BidRequestModels;
using DataAccessLibrary_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.BidService;

public interface IBidService {
    Task<List<Bid>?> GetAllBids();
    Task<Bid?> GetBidById(Guid id);
    Task<List<Bid>?> GetBidsPerListing(Guid id);
    Task<bool> PostBid(AddBidRequest request);
    Task<bool> UpdateBidAmt(UpdateBidRequest request);
    Task<bool> DeleteBidService(DeleteBidRequest request);
}