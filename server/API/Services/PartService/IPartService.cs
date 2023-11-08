using API_AutoAuctioneer.Models.RequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.PartService;

public interface IPartService {
    Task<List<PartEntity>?> GetAllParts();
    Task<List<PartEntity>?> GetOwned(Guid guid);
    Task<PartEntity?> GetPartById(Guid guid);
    Task<bool> AddPart(AddPartRequest request);
    Task<bool> UpdatePart(UpdatePartRequest request);
    Task<bool> DeletePart(DeletePartRequest request);
}