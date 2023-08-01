using API_AutoAuctioneer.Models.CarRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories;

namespace API_AutoAuctioneer.Services.CarService;


public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IUserRepository _userRepository;
    private readonly IListingRepository _listingRepository;
    
    public CarService(ICarRepository carRepository, IUserRepository userRepository, IListingRepository listingRepository)
    {
        _carRepository = carRepository;
        _userRepository = userRepository;
        _listingRepository = listingRepository;
    }
    
    public async Task<List<Car>> GetAllCars() {
        var response = await _carRepository.GetAllCars();
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }

        return response.DataList;
    }

    public async Task<Car> GetCarById(Guid id) {
        var response =  await _carRepository.GetCarById(id);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }

        return response.Data;
    }

    public async Task<List<Car>> GetOwnedCars(Guid id)
    {
        var response = await _carRepository.GetCarsOfSingleUser(id);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }

        return response.DataList;
    }

    public async Task<bool> StoreCar(AddCarRequest request)
    {
        var userResponse = await _userRepository.GetUserById(request.UserId);
        if (!userResponse.IsSuccess)
        {
            Console.WriteLine(userResponse.ErrorMessage);
            return false;
        }else if (userResponse.Data == null)
        {
            Console.WriteLine("No such user is present");
            return false;
        }

        Car car = new Car
        {
            CarId = Guid.NewGuid(),
            Color = request.Color,
            Horsepower = request.Horsepower,
            UserId = request.UserId,
            Make = request.Make,
            Mileage = request.Mileage,
            Model = request.Model,
            Suspension = request.Suspension,
            Torque = request.Torque,
            Transmission = request.Transmission,
            Year = request.Year,
            User = userResponse.Data,
            AftermarketUpgrades = request.AftermarketUpgrades,
            BodyStyle = request.BodyStyle,
            EngineDisplacement = request.EngineDisplacement,
            EngineType = request.EngineType,
            ExteriorFeatures = request.ExteriorFeatures,
            FuelEfficiency = request.FuelEfficiency,
            ImageUrls = request.ImageUrls,
            InteriorColor = request.InteriorColor,
            InteriorFeatures = request.InteriorFeatures,
            OwnershipHistory = request.OwnershipHistory,
            SafetyFeatures = request.SafetyFeatures,
            SeatingCapacity = request.SeatingCapacity,
            ServiceRecords = request.SafetyFeatures,
            TechnologyFeatures = request.TechnologyFeatures,
            AudioAndEntertainment = request.AudioAndEntertainment,
            HasAccidentHistory = request.HasAccidentHistory,
            WheelsAndTires = request.WheelsAndTires,
            NumberOfPreviousOwners = request.NumberOfPreviousOwners,
            VIN = request.VIN,
        };
        var response = await _carRepository.StoreCar(car);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCar(Car request)
    {
        var userResponse = await _userRepository.GetUserById(request.UserId);
        if (!userResponse.IsSuccess)
        {
            Console.WriteLine(userResponse.ErrorMessage);
            return false;
        }else if (userResponse.Data == null)
        {
            Console.WriteLine("User is not present");
            return false;
        }
        
        var response = await _carRepository.GetCarById(request.CarId);
        if (!response.IsSuccess)
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }else if (response.Data == null)
        {
            Console.WriteLine("Car does not exist in the database");
            return false;
        }

        var car = response.Data;
        if (car.UserId != userResponse.Data.UserId)
        {
            Console.WriteLine("The user does not own the car");
            return false;
        }

        car =  request;
        var result = await _carRepository.UpdateCar(car);
        if (!result.IsSuccess)
        {
            Console.WriteLine(result.ErrorMessage);
            return false;
        }
        
        return true;
    }

    public async Task<bool> DeleteCar(DeleteCarRequest request) {
        /*var listing = await _dbContext.Listings.FirstOrDefaultAsync(l => l.Stamp.Equals(stamp));

        _dbContext.Stamps.Remove(stamp);*/
        var car = await GetCarById(request.CarId);
        if (car == null)
        {
            Console.WriteLine("No such car is present in the database");
            return false;
        }else if (car.UserId != request.UserId)
        {
            Console.WriteLine("You do not own the car");
        }
        
        var response = await _carRepository.DeleteCar(car);
        return true;
    }
}