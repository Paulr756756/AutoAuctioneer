using API.Models.DTOs;
using API.Models.RequestModels;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace API.Services.BidService;

public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;
    private readonly IListingRepository _listingRepository;
    private readonly ILogger<BidService> _logger;
    private readonly IUserRepository _userRepository;

    public BidService(IBidRepository bidRepository, IUserRepository userRepository,
        IListingRepository listingRepository,
        ILogger<BidService> logger)
    {
        _bidRepository = bidRepository;
        _userRepository = userRepository;
        _listingRepository = listingRepository;
        _logger = logger;
    }

    public async Task<List<BidDTO>?> GetAllBids()
    {
        var bids = await _bidRepository.GetAllBids();
        var dtos = bids?.Select(bid => new BidDTO
        {
            Id = bid.Id,
            UserId = bid.UserId,
            ListingId = bid.ListingId,
            BidAmount = bid.BidAmount,
            BidTime = bid.BidTime
        }).ToList();
        return dtos;
    }

    public async Task<BidDTO?> GetBidById(Guid id)
    {
        var bid = (BidDTO?)await _bidRepository.GetBidById(id);
        return bid;
    }

    public async Task<List<BidDTO>?> GetOwned(Guid id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            _logger.LogInformation("No such user present in the database");
            return null;
        }

        var bids = await _bidRepository.GetOwned(id);
        var dtos = bids?.Select(bid => new BidDTO
        {
            Id = bid.Id,
            UserId = bid.UserId,
            ListingId = bid.ListingId,
            BidAmount = bid.BidAmount,
            BidTime = bid.BidTime
        }).ToList();
        return dtos;
    }

    public async Task<List<BidDTO>?> GetBidsPerListing(Guid id)
    {
        var bids = await _bidRepository.GetBidsPerListing(id);
        var dtos = bids?.Select(bid => new BidDTO
        {
            Id = bid.Id,
            UserId = bid.UserId,
            ListingId = bid.ListingId,
            BidAmount = bid.BidAmount,
            BidTime = bid.BidTime
        }).ToList();
        return dtos;
    }

    public async Task<List<BidDTO>?> GetBidsOfSingleUserPerListing(BidsListingUserRequest request)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null) return null;
        var listing = await _listingRepository.GetListingById(request.ListingId);
        if (listing?.UserId == user.Id)
        {
            _logger.LogError("Listing Owner trying to bid");
            return null;
        }

        var bids = await _bidRepository.GetBidsPerListing(request.ListingId);
        var dtos = bids?.Select(bid => new BidDTO
        {
            Id = bid.Id,
            UserId = bid.UserId,
            ListingId = bid.ListingId,
            BidAmount = bid.BidAmount,
            BidTime = bid.BidTime
        });
        var result = dtos?.Where(b => b.UserId == request.UserId).ToList();
        return result;
    }

    public async Task<bool> PostBid(AddBidRequest request)
    {
        var user = (UserDTO?)await _userRepository.GetUserById(request.UserId);
        if (user == null)
        {
            _logger.LogInformation("No such user present");
            return false;
        }

        var listing = (ListingDTO?)await _listingRepository.GetListingById(request.ListingId);
        if (listing == null)
        {
            _logger.LogInformation("No such listing exists");
            return false;
        }

        if (listing.UserId == request.UserId)
        {
            _logger.LogInformation("UserEntity trying to post on his own listing");
            return false;
        }

        var dto = (BidDTO)request;
/*        var dto = new BidEntity {
            UserId = request.UserId,
            ListingId = request.ListingId,
            BidAmount = request.BidAmount,
        };*/
        await _bidRepository.PostBid(dto);
        return true;
    }

    public async Task<bool> UpdateBidAmt(UpdateBidRequest request)
    {
        var dto = (BidDTO?)await _bidRepository.GetBidById(request.BidId);
        if (dto == null)
        {
            _logger.LogInformation("No such dto exists");
            return false;
        }

        if (dto.UserId != request.UserId)
        {
            _logger.LogInformation("UserEntity does not own this dto");
            return false;
        }

        dto.BidAmount = request.BidAmount;
        await _bidRepository.UpdateBidAmt(dto);
        return true;
    }

    public async Task<bool> DeleteBidService(DeleteBidRequest request)
    {
        var dto = (BidDTO?)await _bidRepository.GetBidById(request.BidId);
        if (dto == null)
        {
            _logger.LogInformation("No such dto exists");
            return false;
        }

        if (dto.UserId != request.UserId)
        {
            _logger.LogInformation("UserEntity does not own this dto");
            return false;
        }

        await _bidRepository.DeleteBid(dto);
        return true;
    }
}