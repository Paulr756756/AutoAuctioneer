using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<bool> DeleteCar(Guid id);
        Task<List<Car>?> GetAllCars();
        Task<Car>? GetCarById(Guid guid);
        Task<bool> StoreCar(Car car);
        Task<bool> UpdateCar(Car car, Guid id);
    }
}