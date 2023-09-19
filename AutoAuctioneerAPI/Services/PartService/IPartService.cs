using API_AutoAuctioneer.Models.PartRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.PartService;

public interface IPartService {
    Task<List<Part>?> GetAllParts();
    Task<List<Part>?> GetOwned(Guid guid);
    Task<Part?> GetPartById(Guid guid);
    Task<bool> AddPart(AddPartRequest request);
    Task<bool> UpdatePart(UpdatePartRequest request);
    Task<bool> DeletePart(DeletePartRequest request);
}