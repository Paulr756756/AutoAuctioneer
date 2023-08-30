using API_AutoAuctioneer.Models.ListingRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace API_AutoAuctioneer.Services.ListingService;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepository;
    private readonly ICarRepository _carRepository;
    private readonly IPartRepository _carPartRepository;
    private readonly IUserRepository _userRepository;

    public ListingService(IListingRepository listingRepository, IUserRepository userRepository, 
        ICarRepository carRepository, IPartRepository carPartRepository)
    {
        _listingRepository = listingRepository;
        _carRepository = carRepository;
        _carPartRepository = carPartRepository;
        _userRepository = userRepository;
    }

    public async Task<List<Listing>> GetAlListingsService()
    {
        var response = await _listingRepository.GetAllListings();
        if (!response.IsSuccess)
        {
            throw new Exception();
        }
        return response.DataList!;
    }

    public async Task<Listing> GetListingyId(Guid guid)
    {
        var response = await _listingRepository.GetAllListings();
        if (!response.IsSuccess)
        {
            throw new Exception();
        }
        return response.Data!;
    }

    public async Task<List<Listing>> GetOwnedListings(Guid guid) {
        var response = await _listingRepository.GetOwnedListings(guid);
        if(response.IsSuccess) {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }
        return response.DataList!;
    }


    public async Task<bool> AddListingService(ListingRegisterRequest request) {

        var user = (await _userRepository.GetUserById(request.UserId)).Data;
        if (user == null) { return false; }

        if(request.Type == 0) {
            var car = (await _carRepository.GetCarById(request.ItemId)).Data;
            if(car == null) return false;
            var listing = new Listing {
                ListingId = Guid.NewGuid(),
                Car = car,
                CarId = car.CarId,
                User = user,
                UserId = request.UserId
            };
            var response = await _listingRepository.PostListing(listing);
            if (!response.IsSuccess) {
                Console.WriteLine(response.ErrorMessage);
                return false;
            }
            return true;

        } else if(request.Type == 1){
            var part = (await _carPartRepository.GetCarPartById(request.ItemId)).Data;
            if (part == null) { return false; }
            var listing = new Listing {
                ListingId = Guid.NewGuid(),
                CarPart = part,
                CarPartId = part.CarpartId,
                User = user,
                UserId = request.UserId
            };
            var response = await _listingRepository.PostListing(listing);
            if (!response.IsSuccess) {
                Console.WriteLine(response.ErrorMessage);
                return false;
            }

        }
        return false;
    }


    public async Task<bool> DeleteListingService(ListingDeleteRequest request)
    {
        var response = await _listingRepository.GetListingById(request.ListingId);
        if (response.Data == null)
        {
            Console.WriteLine("No such listing exists");
            return false;
        }else if (response.Data.UserId != request.UserId)
        {
            Console.WriteLine("You are not the owner of this listing");
            return false;
        }

        var result = await _listingRepository.DeleteListing(response.Data);
        if(!result.IsSuccess)
        {
            Console.WriteLine(result.ErrorMessage);
            return false;
        }
        
        return true;
    }
}