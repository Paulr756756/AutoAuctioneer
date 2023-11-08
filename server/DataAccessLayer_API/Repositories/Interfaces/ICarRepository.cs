using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<List<CarEntity>?> GetAllCars();
        Task<CarEntity?> GetCarById(Guid guid);
        Task<bool> StoreCar(CarEntity car);
        Task<bool> UpdateCar(CarEntity car);
        Task<List<CarEntity>?> GetCarsOfSingleUser(Guid userId);
        Task<bool> DeleteCar(Guid id);
    }
}