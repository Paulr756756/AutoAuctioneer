using API_AutoAuctioneer.Models.ListingRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.ListingService;

public interface IListingService
{
    Task<List<Listing>> getAlListingsService();

    Task<Listing> getListingyId(Guid guid);

    /*Task<bool> addListingService(ListingRegisterRequest request);*/
    Task<bool> deleteListingService(ListingDeleteRequest request);
}