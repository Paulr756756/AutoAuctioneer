using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IPartRepository : IBaseRepository
    {
        Task<bool> DeletePart(Guid id);
        Task<List<PartEntity>?> GetAllParts();
        Task<PartEntity?> GetPartById(Guid? guid);
        Task<List<PartEntity>?> GetPartsOfSingleUser(Guid guid);
        Task<bool> StorePart(PartEntity part);
        Task<bool> UpdatePart(PartEntity part);
    }
}