using API.Models.RequestModels;
using API.Models.ResponseModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API.Services.ListingService;

public interface IListingService {
    Task<List<ListingEntity>?> GetAlListingsService();
    Task<List<ListingResponse>?> GetAllListingResponses();
    Task<ListingResponse?> GetListingResponseById(Guid id);
    Task<ListingEntity?> GetListingyId(Guid guid);
    Task<List<ListingEntity>?> GetOwnedListings(Guid guid);
    Task<bool> AddListingService(AddListingRequest request);
    Task<bool> DeleteListingService(DeleteListingRequest request);
}