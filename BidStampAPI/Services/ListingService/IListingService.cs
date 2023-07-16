using API_BidStamp.Models.ListingRequestModels;
using DataAccessLibrary_BidStamp.Models;

namespace API_BidStamp.Services.ListingService;

public interface IListingService {
    Task<List<Listing>> getAlListingsService();
    Task<Listing> getListingyId(Guid guid);
    Task<bool> addListingService(ListingRegisterRequest request);
    Task<bool> deleteListingService(ListingDeleteRequest request);
}