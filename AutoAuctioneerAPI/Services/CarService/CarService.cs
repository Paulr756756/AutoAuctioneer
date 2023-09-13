using API_AutoAuctioneer.Models.CarRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace API_AutoAuctioneer.Services.CarService;

public class CarService : ICarService {
    private readonly ICarRepository _carRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CarService> _logger;

    public CarService(ICarRepository carRepository, IUserRepository userRepository, ILogger<CarService> logger) {
        _carRepository = carRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<Car>?> GetAllCars() {
        var carList = await _carRepository.GetAllCars();
        /*if (!response.IsSuccess) {
            Console.WriteLine(response.ErrorMessage);
            throw new Exception();
        }*/

        return carList;
    }

    public async Task<Car?> GetCarById(Guid id) {
        var car = await _carRepository.GetCarById(id);
        if (car==null) {
            _logger.LogInformation("No such car present with id : {id}", id);
            return null;
        }

        return car;
    }

    public async Task<List<Car>?> GetOwnedCars(Guid id) {
        var user = _userRepository.GetUserById(id);
        if(user==null) {
            _logger.LogInformation("No such user present with id : {id}", id);
            return null;
        }
        var carList = await _carRepository.GetCarsOfSingleUser(id);

        return carList;
    }

    public async Task<bool> StoreCar(AddCarRequest request) {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null) {
            _logger.LogInformation("No such user is present with id : {id}", request.UserId);
            return false;
        }

        Car car = new Car { 
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            VIN = request.VIN,
            Color = request.Color,
            BodyType = request.BodyType,
            FuelType = request.FuelType,
            TransmissionType  = request.TransmissionType,
            EngineType = request.EngineType,
            Horsepower = request.Horsepower,
            Torque = request.Torque,
            FuelEfficiency = request.FuelEfficiency,
            Acceleration = request.Acceleration,
            TopSpeed = request.TopSpeed,
            ImageUrls = request.ImageUrls};

        var isSuccess = await _carRepository.StoreCar(car);
        if (!isSuccess) {
            _logger.LogError("Unable to store car");
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateCar(UpdateCarRequest request) {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null) {
            _logger.LogInformation("User does not exist with id : {id}", request.UserId);
            return false;
        }

        var car = await _carRepository.GetCarById(request.CarId);
        if (car == null) {
            _logger.LogInformation("Car does not exist with id : {id}", request.CarId);
            return false;
        }

        /*      TODO() : GetItemById, if item.UserId !=request.UserId, then user does not have ownership
                if (car.UserId != user.Data.UserId) {
                    Console.WriteLine("The user does not own the car");
                    return false;
                }*/
        if(request.Make!=null) car.Make = request.Make;
        if (request.Model!= null) car.Model = request.Model;
        if (request.Year != null) car.Year = request.Year;
        if (request.VIN != null) car.VIN = request.VIN;
        if (request.Color != null) car.Color = request.Color;
        if (request.BodyType != null) car.BodyType = request.BodyType;
        if (request.FuelType != null) car.FuelType = request.FuelType;
        if (request.TransmissionType!= null) car.TransmissionType = request.TransmissionType;
        if (request.EngineType != null) car.EngineType = request.EngineType;
        if (request.Horsepower != null) car.Horsepower = request.Horsepower;
        if (request.Torque != null) car.Torque = request.Torque;
        if (request.FuelEfficiency != null) car.FuelEfficiency = request.FuelEfficiency;
        if (request.Acceleration != null) car.Acceleration = request.Acceleration;
        if (request.TopSpeed != null) car.TopSpeed = request.TopSpeed;
        if (request.ImageUrls != null) car.ImageUrls = request.ImageUrls;

        var result = await _carRepository.UpdateCar(car);

        return result;
    }

    public async Task<bool> DeleteCar(DeleteCarRequest request) {
        var user = await _userRepository.GetUserById(request.UserId);
        if(user != null) {
            _logger.LogError("NO such user present with id : {id}", request.UserId);
            return false;
        }

        var car = await GetCarById(request.CarId);
        if (car == null) {
            _logger.LogError("No such car present with id : {id}", request.CarId);
            return false;
        } 
        
        /*else if (car.UserId != request.UserId) {
            Console.WriteLine("You do not own the car");
        }*/

        var response = await _carRepository.DeleteCar((Guid)car.Id!);

        return response;
    }
}