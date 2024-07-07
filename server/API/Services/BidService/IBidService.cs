using API.Models.DTOs;
using API.Models.RequestModels;

namespace API.Services.BidService;

public interface IBidService
{
    Task<List<BidDTO>?> GetAllBids();
    Task<BidDTO?> GetBidById(Guid id);
    Task<List<BidDTO>?> GetOwned(Guid id);
    Task<List<BidDTO>?> GetBidsPerListing(Guid id);
    Task<List<BidDTO>?> GetBidsOfSingleUserPerListing(BidsListingUserRequest request);
    Task<bool> PostBid(AddBidRequest request);
    Task<bool> UpdateBidAmt(UpdateBidRequest request);
    Task<bool> DeleteBidService(DeleteBidRequest request);
}