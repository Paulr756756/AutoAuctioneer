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
            BidTime = DateTime.UtcNow
        };

        await _bidRepository.PostBid(bid);
        return true;
    }

    public async Task<bool> UpdateBidAmt(UpdateBidRequest request) {
        var response = await _bidRepository.GetBidById(request.BidId);
        if (response.IsSuccess == false) {
            Console.WriteLine(response.ErrorMessage);
            return false;
        } else if (response.Data == null) {
            Console.WriteLine("No such bid exists");
            return false;
        } else if (response.Data.UserId != request.UserId) {
            Console.WriteLine("User does not own this bid");
            return false;
        }
        response.Data.BidAmount = request.BidAmount;
        await _bidRepository.UpdateBidAmt(response.Data);
        return true;
    }

    public async Task<bool> DeleteBidService(DeleteBidRequest request) {
        var response = await _bidRepository.GetBidById(request.BidId);
        if (response.IsSuccess == false) {
            Console.WriteLine(response.ErrorMessage);
            return false;
        } else if (response.Data == null) {
            Console.WriteLine("No such bid exists");
            return false;
        } else if (response.Data.UserId != request.UserId) {
            Console.WriteLine("User does not own this bid");
            return false;
        }

        await _bidRepository.DeleteBid(response.Data);
        return true;
    }
}