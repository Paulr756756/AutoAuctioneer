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
        _logger.LogInformation("Sql statement executed: {e}", sql);

        if (!result.IsSuccess) {
            _logger.LogInformation("Unable to fetch car : {e}", result.ErrorMessage);
        }

        return result.Data!.FirstOrDefault();
    }

    public async Task<List<Car>?> GetAllCars() {
        var sql = "select * from cars";

        var result = await LoadData<Car, dynamic>(sql, new { });
        _logger.LogInformation("Sql statement executed : {m}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("GetAllCars() Error : {e}", result.ErrorMessage);
        }
        return result.Data;
    }

    public async Task<List<Car>?> GetCarsOfSingleUser(Guid userId) {
        var sql = "select cars.* from cars inner join items on cars.id = items.id where items.userid = @Id;";
        var result = await LoadData<Car, dynamic>(sql, new { @Id = userId });
        _logger.LogInformation("Stored procedure executed: {e}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to get cars of single user : {error}", result.ErrorMessage);
        }

        return result.Data;
    }

    public async Task<bool> StoreCar(Car car) {
        var sql = "insert_car";
        var parameters = new DynamicParameters();
        parameters.Add("_id", Guid.NewGuid(),DbType.Guid, ParameterDirection.Output);
        parameters.Add("_userid", car.UserId);
        parameters.Add("_type", car.Type);
        parameters.Add("_make", car.Make);
        parameters.Add("_model", car.Model);
        parameters.Add("_year", car.Year, DbType.Date);
        parameters.Add("_color", car.Color);
        parameters.Add("_vin", car.VIN);
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
        _logger.LogInformation("Stored procedure executed: {e} for id : {id}", sql, parameters.Get<Guid>("_id"));

        if (!result.IsSuccess) {
            _logger.LogError("Unable to store car : {}", result.ErrorMessage);
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
        _logger.LogInformation("Stored procedure executed : {e} for id : {id}", sql, car.Id);

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
        _logger.LogInformation("Stored procedure executed : {e} for id : {id}", sql, id);

        if (!result.IsSuccess) {
            _logger.LogError($"Could not delete car {id}, DeleteCar", id);
            return false;
        }

        return true;
    }
}