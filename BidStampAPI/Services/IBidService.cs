using API_BidStamp.Models.BidRequestModels;
using DataAccessLibrary_BidStamp.Models;

namespace API_BidStamp.Services;

public interface IBidService {
    Task<List<Bid>> getAllBidsService();
    Task<Bid> getBidByIdService(Guid id);
    Task<List<Bid>> getBidsPerListingService(Guid id);
    Task<bool> postBidService(AddBidRequest request);
    Task<bool> updateBidAmtService(UpdateBidRequest request);
    Task<bool> deleteBidService(DeleteBidRequest request);
}