using Dapper;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class CarRepository : BaseRepository, ICarRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<CarRepository> _logger;
    public CarRepository(IConfiguration config, ILogger<CarRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<Car?> GetCarById(Guid carid) {
        var sql = "select * from cars where id=@Id";
        var result = await LoadData<Car, dynamic>(sql, new { Id = carid });

        if (!result.IsSuccess) {
            _logger.LogInformation("Car fetched");
        }

        return result.Data!.FirstOrDefault();
    }

    public async Task<List<Car>?> GetAllCars() {
        var sql = "select * from cars";
        var result = await LoadData<Car, dynamic>(sql, new { });

        if (!result.IsSuccess) {
            _logger.LogError("GetAllCars() Error : {e}", result.ErrorMessage);
        }
        return result.Data;
    }

    public async Task<List<Car>?> GetCarsOfSingleUser(Guid userId) {
        var sql = "get_carsofsingleuser";
        var result = await LoadData<Car, dynamic>(sql, new { _id = userId });
        if (!result.IsSuccess) {
            _logger.LogCritical("Unable to get cars of single user : {error}", result.ErrorMessage);
        }
        return result.Data;
    }

    public async Task<bool> StoreCar(Car car) {
        var sql = "insert_car";
        var parameters = new DynamicParameters();
        parameters.Add("_id", 0, DbType.Guid, ParameterDirection.Output);
        parameters.Add("_make", car.Make);
        parameters.Add("_model", car.Model);
        parameters.Add("_year", car.Year);
        parameters.Add("_vin", car.VIN);
        parameters.Add("_color", car.Color);
        parameters.Add("_bodytype", car.BodyType);
        parameters.Add("_fueltype", car.FuelType);
        parameters.Add("_transmissiontype", car.TransmissionType);
        parameters.Add("_enginetype", car.EngineType);
        parameters.Add("_horsepower", car.Horsepower);
        parameters.Add("_torque", car.Torque);
        parameters.Add("_fuelefficiency", car.FuelEfficiency);
        parameters.Add("_acceleration", car.Acceleration);
        parameters.Add("_topspeed", car.TopSpeed);
        parameters.Add("_imageurls", car.ImageUrls);

        var result = await SaveData(sql, parameters, cmdType: CommandType.StoredProcedure);

        if (!result.IsSuccess) {
            _logger.LogInformation("Unable to store car : {}", result.ErrorMessage);
            return false;
        }
        _logger.LogInformation("Stored car with id :{id}", car.Id);
        return true;
    }

    public async Task<bool> UpdateCar(Car car) {
        var sql = "update_car";
        var parameters = new DynamicParameters();
        parameters.Add("_id", car.Id);
        parameters.Add("_make", car.Make);
        parameters.Add("_model", car.Model);
        parameters.Add("_year", car.Year);
        parameters.Add("_vin", car.VIN);
        parameters.Add("_color", car.Color);
        parameters.Add("_bodytype", car.BodyType);
        parameters.Add("_fueltype", car.FuelType);
        parameters.Add("_transmissiontype", car.TransmissionType);
        parameters.Add("_enginetype", car.EngineType);
        parameters.Add("_horsepower", car.Horsepower);
        parameters.Add("_torque", car.Torque);
        parameters.Add("_fuelefficiency", car.FuelEfficiency);
        parameters.Add("_acceleration", car.Acceleration);
        parameters.Add("_topspeed", car.TopSpeed);
        parameters.Add("_imageurls", car.ImageUrls);

        var result = await SaveData(sql,parameters, cmdType:CommandType.StoredProcedure);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to update car : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
    public async Task<bool> DeleteCar(Guid id) {
        var sql = "delete_item";
        var parameters = new DynamicParameters();
        parameters.Add("_id", id);
        var result = await SaveData(sql, parameters, cmdType: CommandType.StoredProcedure);

        if (!result.IsSuccess) {
            _logger.LogError($"Could not delete car {id}, DeleteCar", id);
            return false;
        }

        return true;
    }
}