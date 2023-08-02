using API_AutoAuctioneer.Models.CarPartRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.CarPartService;

public interface ICarPartService
{
    Task<List<CarPart>> GetAllCarPartsService();
    Task<List<CarPart>> GetOwnedCarPartsService(Guid guid);
    Task<CarPart> GetCarPartById(Guid guid);
    Task<bool> AddCarPart(AddCarPartRequest request);
    Task<bool> UpdateCarPart(UpdateCarPartRequest request);
    Task<bool> DeleteCarPart(DeleteCarPartRequest request);
}