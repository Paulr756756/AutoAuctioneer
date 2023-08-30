using API_AutoAuctioneer.Models.BidRequestModels;
using DataAccessLayer_AutoAuctioneer.Repositories;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using DataAccessLibrary_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services;

public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;
    private readonly IListingRepository _listingRepository;
    private readonly IUserRepository _userRepository;

    public BidService(IBidRepository bidRepository, IUserRepository userRepository,
        IListingRepository listingRepository)
    {
        _bidRepository = bidRepository;
        _userRepository = userRepository;
        _listingRepository = listingRepository;
    }

    public async Task<List<Bid>> GetAllBidsService()
    {
        var response = await _bidRepository.GetAllBids();
        if (!response.IsSuccess)
        {
            throw new Exception();
        }
        return response.DataList;
    }

    public async Task<Bid> GetBidByIdService(Guid id)
    {
        var response = await _bidRepository.GetBidById(id);
        if (!response.IsSuccess)
        {
            throw new Exception();
        }
        return response.Data;
    }

    public async Task<List<Bid>> GetBidsPerListingService(Guid id)
    {
        var response =await _bidRepository.GetBidsPerListing(id);
        if (!response.IsSuccess)
        {
            throw new Exception();
        }
        return response.DataList;
    }

    public async Task<bool> PostBidService(AddBidRequest request)
    {
        var responseUser = await _userRepository.GetUserById(request.UserId);
        if (responseUser.IsSuccess == false)
        {
            Console.WriteLine(responseUser.ErrorMessage);
            return false;
        }else if (responseUser.Data == null)
        {
            Console.WriteLine("No such user exists");
            return false;
        }
        
        var responseListing = await _listingRepository.GetListingById(request.ListingId);
        if (responseListing.IsSuccess == false)
        {
            Console.WriteLine(responseListing.ErrorMessage);
            return false;
        }else if (responseListing.Data == null)
        {
            Console.WriteLine("No such listing exists");
            return false;
        }else if (responseListing.Data.UserId == request.UserId)
        {
            Console.WriteLine("User trying to post on his own listing");
            return false;
        }

        var bid = new Bid
        {
            BidId = Guid.NewGuid(),
            UserId = request.UserId,
            User = responseUser.Data,
            ListingId = request.ListingId,
            Listing = responseListing.Data,
            BidAmount = request.BidAmount,
            BidTime = DateTime.UtcNow
        };

        await _bidRepository.PostBid(bid);
        return true;
    }

    public async Task<bool> UpdateBidAmtService(UpdateBidRequest request)
    {
        var response = await _bidRepository.GetBidById(request.BidId);
        if (response.IsSuccess == false)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }else if (response.Data == null)
        {
            Console.WriteLine("No such bid exists");
            return false;
        }else if (response.Data.UserId != request.UserId)
        {
            Console.WriteLine("User does not own this bid");
            return false;
        }
        response.Data.BidAmount = request.BidAmount;
        await _bidRepository.UpdateBidAmt(response.Data);
        return true;
    }

    public async Task<bool> DeleteBidService(DeleteBidRequest request)
    {
        var response = await _bidRepository.GetBidById(request.BidId);
        if (response.IsSuccess == false)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }else if (response.Data == null)
        {
            Console.WriteLine("No such bid exists");
            return false;
        }else if (response.Data.UserId != request.UserId)
        {
            Console.WriteLine("User does not own this bid");
            return false;
        }
        
        await _bidRepository.DeleteBid(response.Data);
        return true;
    }
}