using API_AutoAuctioneer.Models.BidRequestModels;
using DataAccessLibrary_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services;

public interface IBidService
{
    Task<List<Bid>> GetAllBidsService();
    Task<Bid> GetBidByIdService(Guid id);
    Task<List<Bid>> GetBidsPerListingService(Guid id);
    Task<bool> PostBidService(AddBidRequest request);
    Task<bool> UpdateBidAmtService(UpdateBidRequest request);
    Task<bool> DeleteBidService(DeleteBidRequest request);
}