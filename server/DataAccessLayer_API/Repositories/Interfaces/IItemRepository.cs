using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

public interface IItemRepository
{
    Task<ItemEntity?> GetItemById(Guid id);
    Task<List<ItemEntity>?> GetOwnedItems(Guid id);
    Task<bool> DeleteItem(Guid id);
}