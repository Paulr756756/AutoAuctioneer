using API_AutoAuctioneer.Models.BidRequestModels;
using DataAccessLayer_AutoAuctioneer.Repositories;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using DataAccessLibrary_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.BidService;

public class BidService : IBidService {
    private readonly IBidRepository _bidRepository;
    private readonly IListingRepository _listingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<BidService> _logger;

    public BidService(IBidRepository bidRepository, IUserRepository userRepository,
        IListingRepository listingRepository, ILogger<BidService> logger) {
        _bidRepository = bidRepository;
        _userRepository = userRepository;
        _listingRepository = listingRepository;
        _logger = logger;
    }

    public async Task<List<Bid>?> GetAllBids() {
        var bids = await _bidRepository.GetAllBids();
        return bids;
    }

    public async Task<Bid?> GetBidById(Guid id) {
        var bid = await _bidRepository.GetBidById(id);
        return bid;
    }
    
    public async Task<List<Bid>?> GetOwned(Guid id) {
        var user = _userRepository.GetUserById(id);
        if (user == null) {
            _logger.LogInformation("No such user present in the database");
            return null;
        }

        var bids = await _bidRepository.GetOwned(id);
        return bids;
    }

    public async Task<List<Bid>?> GetBidsPerListing(Guid id) {
        var bids = await _bidRepository.GetBidsPerListing(id);
        return bids;
    }

    public async Task<bool> PostBid(AddBidRequest request) {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null) {
            _logger.LogInformation("No such user present");
            return false;
        }

        var listing = await _listingRepository.GetListingById(request.ListingId);
        if (listing== null) {
            _logger.LogInformation("No such listing exists");
            return false;
        } else if (listing.UserId == request.UserId) {
            _logger.LogInformation("User trying to post on his own listing");
            return false;
        }

        var bid = new Bid {
            UserId = request.UserId,
            ListingId = request.ListingId,
            BidAmount = request.BidAmount,
            /*BidTime = DateTime.Now*/
            /*BidTime = DateTime.UtcNow*/
        };

        await _bidRepository.PostBid(bid);
        return true;
    }

    public async Task<bool> UpdateBidAmt(UpdateBidRequest request) {
        var bid = await _bidRepository.GetBidById(request.BidId);
        if (bid == null) {
            _logger.LogInformation("No such bid exists");
            return false;
        } else if (bid.UserId != request.UserId) {
            _logger.LogInformation("User does not own this bid");
            return false;
        }

        bid.BidAmount = request.BidAmount;
        await _bidRepository.UpdateBidAmt(bid.BidAmount, bid.Id);
        return true;
    }

    public async Task<bool> DeleteBidService(DeleteBidRequest request) {
        var bid = await _bidRepository.GetBidById(request.BidId);
        if (bid == null) {
            _logger.LogInformation("No such bid exists");
            return false;
        } else if (bid.UserId != request.UserId) {
            _logger.LogInformation("User does not own this bid");
            return false;
        }

        await _bidRepository.DeleteBid(bid.Id);
        return true;
    }
}