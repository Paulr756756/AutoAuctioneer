using API_BidStamp.Models.StampRequestModels;
using DataAccessLibrary_BidStamp.Models;

namespace API_BidStamp.Services.StampService {
    public interface IStampService {
        Task<bool> deleteStamp(Guid stamp_id, Guid user_id);
        Task<bool> addStamp(AddStampRequest request, Guid user_id);
        Task<List<Stamp>> getAllStamps();
        Task<Stamp> getStampById(Guid id);
        Task<bool> updateStamp(UpdateStampRequest request, Guid stamp_id, Guid user_id);
    }
}