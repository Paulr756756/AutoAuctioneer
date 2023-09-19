using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetItemById(Guid id);
        Task<List<Item>?> GetOwnedItems(Guid id);
        Task<bool> DeleteItem(Guid id);
    }
}