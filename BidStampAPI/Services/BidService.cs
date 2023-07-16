using API_BidStamp.Models.BidRequestModels;
using DataAccessLayer_BidStamp.Models;
using DataAccessLibrary_BidStamp.Models;
using Repository_BidStamp.Models;

namespace API_BidStamp.Services;

public class BidService : IBidService {
    private readonly IBidRepository _bidRepository;
    private readonly IListingRepository _listingRepository;
    private readonly IUserRepository _userRepository;

    public BidService(IBidRepository bidRepository, IUserRepository userRepository,
        IListingRepository listingRepository) {
        _bidRepository = bidRepository;
        _userRepository = userRepository;
        _listingRepository = listingRepository;
    }

    public async Task<List<Bid>> getAllBidsService() {
        return await _bidRepository.getAllBids();
    }

    public async Task<Bid> getBidByIdService(Guid id) {
        return await _bidRepository.getBidById(id);
    }

    public async Task<List<Bid>> getBidsPerListingService(Guid id) {
        return await _bidRepository.getBidsPerListing(id);
    }

    public async Task<bool> postBidService(AddBidRequest request) {
        var user = await _userRepository.getUserById(request.UserId);
        if (user == null) return false;
        var listing = await _listingRepository.getListingById(request.ListingId);
        if (listing == null) return false;
        if (listing.UserId == request.UserId) return false;

        var bid = new Bid {
            BidId = Guid.NewGuid(),
            UserId = request.UserId,
            User = user,
            ListingId = request.ListingId,
            Listing = listing,
            BidAmount = request.BidAmount,
            BidTime = DateTime.UtcNow
        };

        await _bidRepository.postBid(bid);
        return true;
    }

    public async Task<bool> updateBidAmtService(UpdateBidRequest request) {
        var bid = await _bidRepository.getBidById(request.BidId);
        if (bid == null) return false;
        if (bid.UserId != request.UserId) return false;

        bid.BidAmount = request.BidAmount;
        await _bidRepository.updateBidAmt(bid);
        return true;
    }

    public async Task<bool> deleteBidService(DeleteBidRequest request) {
        var bid = await _bidRepository.getBidById(request.BidId);
        if (bid == null) return false;
        if (bid.UserId != request.UserId) return false;

        await _bidRepository.deleteBid(bid);
        return true;
    }
}