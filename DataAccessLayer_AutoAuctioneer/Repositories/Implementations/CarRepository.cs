using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class CarRepository : BaseRepository, ICarRepository
{
    private readonly IConfiguration _config;
    public CarRepository(IConfiguration config) : base(config)
    {
        _config = config;
    }

    public async Task<Car>? GetCarById(Guid guid)
    {
        var sql = /*"select * from (" +*/
            "select \"cars\".id as primaryId, * from \"cars\" ;";
        /*+ "inner join \"cars_performance_handlind\" " +
        " on \"cars\".id = \"cars_performance_handling\".id) as car where \"car\".primaryId = @Id ";*/
        var result = await LoadData<Car, dynamic>(sql, new { Id = guid });

        if (!result.IsSuccess)
        {
            //Log
        }

        return result.Data!.First();
    }

    public async Task<List<Car>?> GetAllCars()
    {
        var sql = "select \"cars\".id as primaryId, * from \"cars\" ;";
        /*+ " inner join \"cars_performance_handlind\" on \"cars\".id = \"cars_performance_handling\".id ;";*/
        var result = await LoadData<Car, dynamic>(sql, new { });

        if (!result.IsSuccess)
        {
            //Log
        }
        return result.Data;
    }

    /*    public async Task<List<Car>?> GetCarsOfSingleUser(Guid guid)
        {
            var sql = "select * from (" +
            "select \"cars\".id as primaryId, * from \"cars\" inner join \"cars_performance_handlind\" " +
            "on \"cars\".id = \"cars_performance_handling\".id"
        }*/

    public async Task<bool> StoreCar(Car car)
    {
        var sql = "insert into \"cars\" (id, make, model, year, vin, color, bodytype, fueltype, " +
            "transmissiontype,enginetype, horsepower, torque, fuelefficiency, acceleration, topspeed, imageurls) values" +
            "(@Id, @Make, @Model,  @Year, @Vin, @Color, @BodyType, @FuelType, @TransmissionType, @EngineType, @HorsePower,"
            + "@Torque, @FuelEfficiency, @Acceleration, @TopSpeed, @ImageUrls) ;";
        var result = await SaveData<dynamic>(sql, new
        {
            Id = car.CarId,
            car.Make,
            car.Model,
            car.Year,
            Vin = car.VIN,
            car.Color,
            car.BodyType,
            car.FuelType,
            car.TransmissionType,
            car.EngineType,
            HorsePower = car.Horsepower,
            car.Torque,
            car.FuelEfficiency,
            car.Acceleration,
            car.TopSpeed,
            car.ImageUrls
        });

        if (!result.IsSuccess)
        {
            //Log
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateCar(Car car, Guid id)
    {
        var sql = "update \"cars\" set " +
             "make=@Make, model=@Model, year=@Year, vin=@Vin, color=@Color, bodytype=@Bodytype, fueltype=@FuelType" +
             "transmissiontype=@TransmissionType, enginetype=@EngineType, horsepower=@HorsePower, torque=@Torque," +
             "fuelefficiency=@FuelEfficiency, acceleration=@Acceleration, topspeed=@TopSpeed, imageurls=@ImageUrls" +
             "where id=@Id";
        var result = await SaveData<dynamic>(sql, new
        {
            Id=id,
            car.Make,
            car.Model,
            car.Year,
            Vin = car.VIN,
            car.Color,
            car.BodyType,
            car.FuelType,
            car.TransmissionType,
            car.EngineType,
            HorsePower = car.Horsepower,
            car.Torque,
            car.FuelEfficiency,
            car.Acceleration,
            car.TopSpeed,
            car.ImageUrls
        });

        if (!result.IsSuccess)
        {
            //Log
            return false;
        }
        return true;
    }
    public async Task<bool> DeleteCar(Guid id)
    {
        var sql = "delete from \"cars\" where id=@Id";
        var result = await SaveData<dynamic>(sql, new { Id = id });

        if (!result.IsSuccess)
        {
            //Log
            return false;
        }
        return true;
    }
}