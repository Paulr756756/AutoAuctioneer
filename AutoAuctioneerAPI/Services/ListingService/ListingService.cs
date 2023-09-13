using API_AutoAuctioneer.Models.ListingRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace API_AutoAuctioneer.Services.ListingService;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ListingService> _logger;  

    public ListingService(IListingRepository listingRepository, IUserRepository userRepository,
        ILogger<ListingService> logger, IItemRepository itemRepository)
    {
        _listingRepository = listingRepository;
        _userRepository = userRepository;
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task<List<Listing>?> GetAlListingsService()
    {
        var listings = await _listingRepository.GetAllListings();
        return listings;
    }

    public async Task<Listing?> GetListingyId(Guid guid)
    {
        var listing = await _listingRepository.GetListingById(guid);
        if (listing == null) {
            _logger.LogInformation("NO such listing present in the database with id : {id}", guid);
        }
        return listing;
    }

    public async Task<List<Listing>?> GetOwnedListings(Guid guid) {
        var user = await _userRepository.GetUserById(guid); 
        if (user == null) {
            _logger.LogInformation("NO such user present in the database");
            return null;
        }
        var listings = await _listingRepository.GetOwnedListings(guid);
        return listings;
    }


    public async Task<bool> AddListingService(AddListingRequest request) {

        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null) {
            _logger.LogInformation("NO such user present in the database");
            return false; 
        }
        var item = await _itemRepository.GetItemById(request.ItemId);
        if (item == null) {
            _logger.LogInformation("No such item found in the database");
            return false;
        }
        var listing = new Listing {
            UserId = request.UserId,
            ItemId = request.ItemId
        };
        var result = await _listingRepository.PostListing(listing);

        return result;
    }


    public async Task<bool> DeleteListingService(ListingDeleteRequest request)
    {
        var listing = await _listingRepository.GetListingById(request.Id);
        if (listing == null)
        {
            _logger.LogInformation("No such listing exists");
            return false;
        }else if (listing.UserId != request.UserId)
        {
            _logger.LogInformation("You are not the owner of this listing");
            return false;
        }

        var result = await _listingRepository.DeleteListing(listing.Id);
        
        return result;
    }
}