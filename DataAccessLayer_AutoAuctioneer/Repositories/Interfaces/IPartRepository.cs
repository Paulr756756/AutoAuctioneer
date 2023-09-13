using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IPartRepository : IBaseRepository
    {
        Task<bool> DeletePart(Guid id);
        Task<List<Part>?> GetAllParts();
        Task<Part?> GetPartById(Guid guid);
        Task<List<Part>?> GetPartsOfSingleUser(Guid guid);
        Task<bool> StorePart(Part part);
        Task<bool> UpdatePart(Part part);
    }
}