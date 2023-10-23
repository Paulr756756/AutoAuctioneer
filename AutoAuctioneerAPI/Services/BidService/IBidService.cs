using DataAccessLibrary_AutoAuctioneer.Models;
using API_AutoAuctioneer.Models.RequestModels;
using API_AutoAuctioneer.Models.DTOs;

namespace API_AutoAuctioneer.Services.BidService;

public interface IBidService {
    Task<List<BidDTO>?> GetAllBids();
    Task<BidDTO?> GetBidById(Guid id);
    Task<List<BidDTO>?> GetOwned(Guid id);
    Task<List<BidDTO>?> GetBidsPerListing(Guid id);
    Task<bool> PostBid(AddBidRequest request);
    Task<bool> UpdateBidAmt(UpdateBidRequest request);
    Task<bool> DeleteBidService(DeleteBidRequest request);
}