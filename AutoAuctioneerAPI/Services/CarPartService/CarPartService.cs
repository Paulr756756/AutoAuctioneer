using API_AutoAuctioneer.Models.CarPartRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories;

namespace API_AutoAuctioneer.Services.CarPartService;

public class CarPartService: ICarPartService
{
    private readonly ICarPartRepository _carPartRepository;
    private readonly IEngineRepository _engineRepository;
    private readonly ICustomizationCarPartRepository _customizationCarPartRepository;
    private readonly IIndividualCarPartRepository _individualCarPartRepository;
    private readonly IUserRepository _userRepository;

    public CarPartService(ICarPartRepository carPartRepository, IEngineRepository engineRepository,
        ICustomizationCarPartRepository customizationCarPartRepository, IIndividualCarPartRepository 
            individualCarPartRepository, IUserRepository userRepository)
    {
        _carPartRepository = carPartRepository;
        _engineRepository = engineRepository;
        _customizationCarPartRepository = customizationCarPartRepository;
        _individualCarPartRepository = individualCarPartRepository;
        _userRepository = userRepository;
    }

    public async Task<List<CarPart>> GetAllCarPartsService()
    {
        var response = await _carPartRepository.GetAllCarParts();
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }

        return response.DataList;
    }

    public async Task<List<CarPart>> GetOwnedCarPartsService(Guid guid)
    {
        var userResponse = await _userRepository.GetUserById(guid);
        if (!userResponse.IsSuccess)
        {
            Console.WriteLine(userResponse.ErrorMessage);
            throw new Exception();
        }else if (userResponse.Data == null)
        {
            Console.WriteLine("User does not exist.");
            throw new Exception();
        }

        var response = await _carPartRepository.GetCarPartOfSingleUser(guid);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }

        return response.DataList;
    }

    public async Task<CarPart> GetCarPartById(Guid guid)
    {
        var response = await _carPartRepository.GetCarPartById(guid);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }

        return response.Data;
    }

    public async Task<bool> AddCarPart(AddCarPartRequest request)
    {
        var userResponse = await _userRepository.GetUserById(request.UserId);
        if (!userResponse.IsSuccess)
        {
            Console.WriteLine(userResponse.ErrorMessage);
            throw new Exception();
        }else if (userResponse.Data == null)
        {
            Console.WriteLine("User does not exist.");
            throw new Exception();
        }

        var part = new CarPart
        {
            Name = request.Name,
            UserId = request.UserId,
            User = userResponse.Data,
            PartType = request.PartType,
            Description = request.Description,
            MarketPrice = request.MarketPrice,
            CarpartId = Guid.NewGuid(),
        };

        var response = await _carPartRepository.StoreCarPartAsync(part);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCarPart(UpdateCarPartRequest request)
    {
        var userResponse = await _userRepository.GetUserById(request.UserId);
        if (!userResponse.IsSuccess)
        {
            Console.WriteLine(userResponse.ErrorMessage);
            return false;
        }else if (userResponse.Data == null)
        {
            Console.WriteLine("User does not exist.");
            return false;
        }

        var response = await _carPartRepository.GetCarPartById(request.CarpartId);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }else if (response.Data == null)
        {
            Console.WriteLine("Part does not exist.");
            return false;
        }

        if (request.UserId != response.Data.UserId)
        {
            Console.WriteLine("User does not own this part");
            return false;
        }

        var part = response.Data;
        part.PartType = request.PartType;
        part.Description = request.Description;
        part.MarketPrice = request.MarketPrice;
        part.Name = request.Name;

        var result = await _carPartRepository.UpdateCarPart(part);
        if (!result.IsSuccess)
        {
            Console.WriteLine(result.ErrorMessage);
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteCarPart(DeleteCarPartRequest request)
    {
        var userResponse = await _userRepository.GetUserById(request.UserId);
        if (!userResponse.IsSuccess)
        {
            Console.WriteLine(userResponse.ErrorMessage);
            return false;
        }else if (userResponse.Data == null)
        {
            Console.WriteLine("User does not exist.");
            return false;
        }

        var response = await _carPartRepository.GetCarPartById(request.CarPartId);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }else if (response.Data == null)
        {
            Console.WriteLine("Part does not exist.");
            return false;
        }

        if (request.UserId != response.Data.UserId)
        {
            Console.WriteLine("User does not own this part");
            return false;
        }

        var result = await _carPartRepository.DeleteCarPartAsync(response.Data);
        if (!result.IsSuccess)
        {
            Console.WriteLine(result.ErrorMessage);
            return false;
        }

        return true;
    }
}