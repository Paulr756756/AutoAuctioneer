using System.Linq.Expressions;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface ICarRepository
{
    Task<Result<Car>> GetCarById(Guid guid);
    Task<Result<Car>> GetAllCars();
    Task<Result<Car>> GetCarsOfSingleUser(Guid guid);
    Task<Result<Car>> StoreCar(Car car);
    Task<Result<Car>> UpdateCar(Car car);
    Task<Result<Car>> DeleteCar(Car car);
}

public class CarRepository : BaseRepository<Car>,ICarRepository
{
    private readonly DatabaseContext _dbContext;
    public CarRepository(DatabaseContext dbContext) :base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Car>> GetCarById(Guid guid)
    {
        return await GetSingleItemAsync(e => e.CarId == guid);
    }

    public async Task<Result<Car>> GetAllCars()
    {
         return await GetALlItemsAsync();
    }

    public async Task<Result<Car>> GetCarsOfSingleUser(Guid guid)
    {
        return await GetMultipleItemsAsync(e => e.UserId == guid);
    }

    public async Task<Result<Car>> StoreCar(Car car)
    {
        return await StoreItemAsync(car);
    }

    public async Task<Result<Car>> UpdateCar(Car car)
    {
        return await UpdateItemAsync(car);
    }
    public async Task<Result<Car>> DeleteCar(Car car)
    {
        return await DeleteItemAsync(car);
    }
}