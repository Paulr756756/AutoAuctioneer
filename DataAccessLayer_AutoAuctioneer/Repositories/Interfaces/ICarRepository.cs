using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>?> GetAllCars();
        Task<Car?> GetCarById(Guid guid);
        Task<bool> StoreCar(Car car);
        Task<bool> UpdateCar(Car car);
        Task<List<Car>?> GetCarsOfSingleUser(Guid userId);
        Task<bool> DeleteCar(Guid id);
    }
}