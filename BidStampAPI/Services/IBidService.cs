using API_AutoAuctioneer.Models.BidRequestModels;
using DataAccessLibrary_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services;

public interface IBidService
{
    Task<List<Bid>> getAllBidsService();
    Task<Bid> getBidByIdService(Guid id);
    Task<List<Bid>> getBidsPerListingService(Guid id);
    Task<bool> postBidService(AddBidRequest request);
    Task<bool> updateBidAmtService(UpdateBidRequest request);
    Task<bool> deleteBidService(DeleteBidRequest request);
}