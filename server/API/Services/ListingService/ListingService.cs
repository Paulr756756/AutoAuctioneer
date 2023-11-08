using API_AutoAuctioneer.Models.RequestModels;
using API_AutoAuctioneer.Models.ResponseModels;
using API_AutoAuctioneer.Services.BidService;
using API_AutoAuctioneer.Services.CarService;
using API_AutoAuctioneer.Services.ItemService;
using API_AutoAuctioneer.Services.PartService;
using API_AutoAuctioneer.Services.UserService;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;


namespace API_AutoAuctioneer.Services.ListingService;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService userService;
    private readonly ILogger<ListingService> _logger;
    private readonly IBidRepository _bidRepository;
    private readonly IItemService _itemService;
    private readonly ICarService _carService;
    private readonly IPartService _partService;

    public ListingService(IListingRepository listingRepository, IUserRepository userRepository,ILogger<ListingService> logger,
        IItemRepository itemRepository, IBidRepository bidRepository, IItemService itemService, ICarService carService, IPartService partService)
    {
        _listingRepository = listingRepository;
        _userRepository = userRepository;
        _itemRepository = itemRepository;
        _logger = logger;
         _bidRepository = bidRepository;
        _itemService = itemService;
        _carService = carService;
        _partService = partService;
    }

    public async Task<List<ListingEntity>?> GetAlListingsService()
    {
        var listings = await _listingRepository.GetAllListings();
        return listings;
    }

    public async Task<ListingEntity?> GetListingyId(Guid guid)
    {
        var listing = await _listingRepository.GetListingById(guid);
        if (listing == null) {
            _logger.LogInformation("NO such listing present in the database with id : {id}", guid);
        }
        return listing;
    }

    public async Task<List<ListingEntity>?> GetOwnedListings(Guid guid) {
        var user = await _userRepository.GetUserById(guid); 
        if (user == null) {
            _logger.LogInformation("NO such user present in the database");
            return null;
        }
        var listings = await _listingRepository.GetOwnedListings(guid);
        return listings;
    }
    public async Task<List<ListingResponse>?> GetAllListingResponses() {
        var entities = await GetAlListingsService();
        if(entities == null) return null; 
        var response = new List<ListingResponse>();
        foreach(var entity in entities) {
            var listing = (ListingResponse?)entity;
            var user = await userService.GetUserById(entity.UserId);
            listing!.UserName = user!.UserName!;
            var bids = await _bidRepository.GetBidsPerListing(entity.Id);
            var highestBid = bids?.MaxBy(b=>b.BidAmount);
            var bidCount = bids == null ? 0 : bids.Count();
            listing!.HighestBidAmt = highestBid!.BidAmount;
            listing!.BidCount = bidCount;
            var item = await _itemService.GetById(entity.ItemId);
            listing!.ItemType = item!.Type;
            if (item?.Type == 0) {
                var car = await _carService.GetCarById(entity.ItemId);
                listing!.Make = car!.Make;
                listing!.Model = car.Model;
                listing!.Year = car.Year;
                
            }else {
                var part = await _partService.GetPartById(entity.ItemId);
                listing!.Name = part!.Name;
                listing!.Description = part.Description;
            }
            response.Add(listing);
        }
        return response;
    }
    public async Task<ListingResponse?> GetListingResponseById(Guid id) {
        var entity = await GetListingyId(id);
        var user = await userService.GetUserById(entity!.UserId);
        var bids = await _bidRepository.GetBidsPerListing(entity.Id);
        var highestBid = bids?.MaxBy(b => b.BidAmount);
        var bidCount = bids==null?0:bids.Count();
        var item = await _itemService.GetById(entity.ItemId);
        var response = (ListingResponse?)entity;
        response!.ItemType = item!.Type;
        response.BidCount = bidCount;
        if(item?.Type == 0) {
            var car = await _carService.GetCarById(entity.ItemId);
            response!.Make = car!.Make;
            response!.Model = car.Model;
            response!.Year = car.Year;
            response!.ImageUrl = car.ImageUrls?[0];
        } else {
            var part = await _partService.GetPartById(entity.ItemId);
            response!.Name = part!.Name;
            response!.Description = part.Description;
        }
        return response;
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
        var listing = new ListingEntity {
            UserId = request.UserId,
            ItemId = request.ItemId
        };
        var result = await _listingRepository.PostListing(listing);

        return result;
    }


    public async Task<bool> DeleteListingService(DeleteListingRequest request)
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