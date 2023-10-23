using API_AutoAuctioneer.Models.RequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.CarService;

public interface ICarService {
    Task<List<CarEntity>?> GetAllCars();
    Task<CarEntity?> GetCarById(Guid id);
    Task<List<CarEntity>?> GetOwnedCars(Guid id);
    Task<bool> StoreCar(AddCarRequest request);
    Task<bool> UpdateCar(UpdateCarRequest request);
    Task<bool> DeleteCar(DeleteCarRequest request);

}