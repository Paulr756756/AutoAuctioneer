using API_BidStamp.Models.StampRequestModels;
using DataAccessLayer_BidStamp;
using DataAccessLayer_BidStamp.Models;
using DataAccessLayer_BidStamp.Repositories;
using DataAccessLibrary_BidStamp.Models;

namespace API_BidStamp.Services.StampService;

public class StampService : IStampService {
    private readonly IStampRepository _stamp_repository;
    private readonly IUserRepository _user_repository;

    public StampService(IStampRepository stamp_repository, IUserRepository user_repository) {
        _stamp_repository = stamp_repository;
        _user_repository = user_repository;
    }

    public async Task<List<Stamp>> getAllStamps() {
        return await _stamp_repository.getAllStamps();
    }

    public async Task<Stamp> getStampById(Guid id) {
        return await _stamp_repository.getStampById(id);
    }

    public async Task<bool> addStamp(AddStampRequest request, Guid user_id) {
        var user = await _user_repository.getUserById(user_id);
        if (user == null) return false;

        var stamp = new Stamp {
            StampId = Guid.NewGuid(),
            StampTitle = request.StampTitle,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Year = request.Year,
            Country = request.Country,
            Condition = request.Condition,
            CatalogNumber = request.CatalogNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            User = user,
            UserId = user_id
        };

        await _stamp_repository.storeStamp(stamp);
        return true;
    }

    public async Task<bool> updateStamp(UpdateStampRequest request, Guid stamp_id, Guid user_id) {
        var stamp = await _stamp_repository.getStampById(stamp_id);
        if (stamp == null) return false;
        if (user_id != stamp.UserId) return false;

        stamp.StampTitle = request.StampTitle;
        stamp.Description = request.Description;
        stamp.ImageUrl = request.ImageUrl;
        stamp.Year = request.Year;
        stamp.Country = request.Country;
        stamp.Condition = request.Condition;
        stamp.CatalogNumber = request.CatalogNumber;
        stamp.StartDate = request.StartDate;
        stamp.EndDate = request.EndDate;

        await _stamp_repository.updateStamp(stamp);
        return true;
    }

    public async Task<bool> deleteStamp(Guid stamp_id, Guid user_id) {
        var stamp = await getStampById(stamp_id);
        if (stamp == null) return false;
        if (user_id != stamp.UserId) return false;

        await _stamp_repository.deleteStamp(stamp);
        return true;
    }
}