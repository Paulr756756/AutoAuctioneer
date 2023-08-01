using API_AutoAuctioneer.Models.ListingRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories;

namespace API_AutoAuctioneer.Services.ListingService;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepository;

    /*private readonly IStampRepository _stampRepository;*/
    private readonly IUserRepository _userRepository;

    public ListingService(IListingRepository listingRepository, /* IStampRepository
        stampRepository,*/ IUserRepository userRepository)
    {
        _listingRepository = listingRepository;
        /*_stampRepository = stampRepository;*/
        _userRepository = userRepository;
    }

    public async Task<List<Listing>> getAlListingsService()
    {
        return await _listingRepository.GetAllListings();
    }

    public async Task<Listing> getListingyId(Guid guid)
    {
        return await _listingRepository.GetListingById(guid);
    }

    /*
    public async Task<bool> addListingService(ListingRegisterRequest request) {
        var stamp = await _stampRepository.getStampById(request.StampId);
        if (stamp == null)
            return false;
        if (stamp.Listing != null)
            return false;
        if (stamp.UserId != request.UserId) return false;

        var listing = new Listing {
            ListingId = Guid.NewGuid(),
            Stamp = stamp,
            User = await _userRepository.getUserById(request.UserId),
            UserId = request.UserId
        };

        listing.Stamp.Listing = listing;
        listing.StampId = request.StampId;

        await _listingRepository.PostListing(listing);
        return true;
    }
    */

    public async Task<bool> deleteListingService(ListingDeleteRequest request)
    {
        var listing = await _listingRepository.GetListingById(request.ListingId);
        if (listing == null)
            return false;
        if (listing.UserId != request.UserId) return false;

        await _listingRepository.DeleteListing(listing);
        return true;
    }
}