using Dapper;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class CarRepository : BaseRepository, ICarRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<CarRepository> _logger;
    public CarRepository(IConfiguration config, ILogger<CarRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<CarEntity?> GetCarById(Guid carid) {
        var sql = "select * from cars where id=@Id";

        var result = await LoadData<CarEntity, dynamic>(sql, new { Id = carid });
        _logger.LogInformation("Sql statement executed: {e}", sql);

        if (!result.IsSuccess) {
            _logger.LogInformation("Unable to fetch car : {e}", result.ErrorMessage);
        }

        return result.Data!.FirstOrDefault();
    }

    public async Task<List<CarEntity>?> GetAllCars() {
        var sql = "select * from cars";

        var result = await LoadData<CarEntity, dynamic>(sql, new { });
        _logger.LogInformation("Sql statement executed : {m}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("GetAllCars() Error : {e}", result.ErrorMessage);
        }
        return result.Data;
    }

    public async Task<List<CarEntity>?> GetCarsOfSingleUser(Guid userId) {
        var sql = "select cars.* from cars inner join items on cars.id = items.id where items.userid = @Id;";
        var result = await LoadData<CarEntity, dynamic>(sql, new { @Id = userId });
        _logger.LogInformation("Stored procedure executed: {e}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to get cars of single user : {error}", result.ErrorMessage);
        }

        return result.Data;
    }

    public async Task<bool> StoreCar(CarEntity car) {
        /*const string sql = "insert_car";
        var parameters = new DynamicParameters();
        parameters.Add("_id", Guid.NewGuid(),DbType.Guid, ParameterDirection.Output);
        parameters.Add("_userid", car.UserId, DbType.Guid);
        parameters.Add("_type", car.Type);
        parameters.Add("_make", car.Make, DbType.String);
        parameters.Add("_model", car.Model, DbType.String);
        parameters.Add("_year", car.Year);
        parameters.Add("_color", car.Color);
        parameters.Add("_vin", car.VIN, DbType.String);
        parameters.Add("_bodytype", car.BodyType);
        parameters.Add("_fueltype", car.FuelType);
        parameters.Add("_transmissiontype", car.TransmissionType);
        parameters.Add("_enginetype", car.EngineType);
        parameters.Add("_horsepower", car.Horsepower);
        parameters.Add("_torque", car.Torque);
        parameters.Add("_fuelefficiency", car.FuelEfficiency);
        parameters.Add("_acceleration", car.Acceleration);
        parameters.Add("_topspeed", car.TopSpeed);
        parameters.Add("_imageurls", car.ImageUrls);*/
        
        var command = new NpgsqlCommand() {
            CommandText = "insert_car",
            CommandType = CommandType.StoredProcedure,
        };

        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, Guid.NewGuid()).Direction = ParameterDirection.Output;
        command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, car.UserId);
        command.Parameters.AddWithValue("_type", NpgsqlDbType.Integer, car.Type);
        command.Parameters.AddWithValue("_make", NpgsqlDbType.Text, car.Make);
        command.Parameters.AddWithValue("_model", NpgsqlDbType.Text, car.Model);
        command.Parameters.AddWithValue("_year", NpgsqlDbType.Varchar, car.Year == null ? DBNull.Value : car.Year);
        command.Parameters.AddWithValue("_color", NpgsqlDbType.Varchar, car.Color == null ? DBNull.Value : car.Color);
        command.Parameters.AddWithValue("_vin", NpgsqlDbType.Text, car.VIN == null ? DBNull.Value : car.VIN);
        command.Parameters.AddWithValue("_bodytype", NpgsqlDbType.Varchar, car.BodyType == null ? DBNull.Value : car.BodyType);
        command.Parameters.AddWithValue("_fueltype", NpgsqlDbType.Varchar, car.FuelType == null ?DBNull.Value : car.FuelType);
        command.Parameters.AddWithValue("_transmissiontype", NpgsqlDbType.Varchar, car.TransmissionType == null ? DBNull.Value : car.TransmissionType);
        command.Parameters.AddWithValue("_enginetype", NpgsqlDbType.Varchar, car.EngineType == null ? DBNull.Value : car.EngineType);
        command.Parameters.AddWithValue("_horsepower", NpgsqlDbType.Integer, car.Horsepower == null ? DBNull.Value : car.Horsepower);
        command.Parameters.AddWithValue("_torque", NpgsqlDbType.Integer,car.Torque == null ? DBNull.Value : car.Torque);
        command.Parameters.AddWithValue("_fuelefficiency", NpgsqlDbType.Real, car.FuelEfficiency == null ?DBNull.Value : car.FuelEfficiency);
        command.Parameters.AddWithValue("_acceleration", NpgsqlDbType.Real, car.Acceleration == null ? DBNull.Value : car.Acceleration);
        command.Parameters.AddWithValue("_topspeed",NpgsqlDbType.Integer,car.TopSpeed == null ? DBNull.Value : car.TopSpeed);
        command.Parameters.AddWithValue(parameterName: "_imageurls",
            parameterType: NpgsqlDbType.Array | NpgsqlDbType.Text,
            value: car.ImageUrls == null ? DBNull.Value : car.ImageUrls);
        

        // var result = await SaveData(sql, parameters, cmdType: CommandType.StoredProcedure);
        var result = await SaveData<CarEntity>(command);
        // _logger.LogInformation("Stored procedure executed: {e} for id : {id}", command);
        if (!result.IsSuccess) {
            _logger.LogError("Unable to store car : {}", result.ErrorMessage);
            return false;
        }
        _logger.LogInformation("Stored car with id :{id}", car.Id);
        return true;
    }

    public async Task<bool> UpdateCar(CarEntity car) {
        var sql = "update_car";
/*        var parameters = new DynamicParameters();
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
        parameters.Add("_imageurls", car.ImageUrls);*/

        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, car.Id!);
        command.Parameters.AddWithValue("_make", NpgsqlDbType.Text, car.Make == null ? DBNull.Value : car.Make);
        command.Parameters.AddWithValue("_model", NpgsqlDbType.Text, car.Model == null ? DBNull.Value : car.Model);
        command.Parameters.AddWithValue("_year", NpgsqlDbType.Varchar, car.Year == null ? DBNull.Value : car.Year);
        command.Parameters.AddWithValue("_vin", NpgsqlDbType.Text, car.VIN == null ? DBNull.Value : car.VIN);
        command.Parameters.AddWithValue("_color", NpgsqlDbType.Varchar, car.Color == null ? DBNull.Value : car.Color);
        command.Parameters.AddWithValue("_bodytype", NpgsqlDbType.Varchar, car.BodyType == null ? DBNull.Value : car.BodyType);
        command.Parameters.AddWithValue("_fueltype", NpgsqlDbType.Varchar, car.FuelType == null ? DBNull.Value : car.FuelType);
        command.Parameters.AddWithValue("_transmissiontype", NpgsqlDbType.Varchar, car.TransmissionType == null ? DBNull.Value : car.TransmissionType);
        command.Parameters.AddWithValue("_enginetype", NpgsqlDbType.Varchar, car.EngineType == null ? DBNull.Value : car.EngineType);
        command.Parameters.AddWithValue("_horsepower", NpgsqlDbType.Integer, car.Horsepower == null ? DBNull.Value : car.Horsepower);
        command.Parameters.AddWithValue("_torque", NpgsqlDbType.Integer, car.Torque==null?DBNull.Value:car.Torque);
        command.Parameters.AddWithValue("_fuelefficiency", NpgsqlDbType.Real, car.FuelEfficiency==null?DBNull.Value:car.FuelEfficiency);
        command.Parameters.AddWithValue("_acceleration", NpgsqlDbType.Real, car.Acceleration==null?DBNull.Value:car.Acceleration);
        command.Parameters.AddWithValue("_topspeed", NpgsqlDbType.Integer, car.TopSpeed==null?DBNull.Value:car.TopSpeed);
        command.Parameters.AddWithValue("_imageurls", NpgsqlDbType.Text | NpgsqlDbType.Array, car.ImageUrls==null?DBNull.Value:car.ImageUrls);


        /*var result = await SaveData(sql,parameters, cmdType:CommandType.StoredProcedure);*/
        var result = await SaveData<CarEntity>(command);
        _logger.LogInformation("Stored procedure executed : {e} for id : {id}", sql, car.Id);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to update car : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
    public async Task<bool> DeleteCar(Guid id) {
        var sql = "delete_item";
        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure,
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

        var result = await SaveData<CarEntity>(command);
        _logger.LogInformation("Stored procedure executed : {e} for id : {id}", sql, id);

        if (!result.IsSuccess) {
            _logger.LogError($"Could not delete car {id}, DeleteCar", id);
            return false;
        }

        return true;
    }
}