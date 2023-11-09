using DataAccessLayer_AutoAuctioneer.Models;

namespace API.Services.ItemService;

public interface IItemService {
    Task<ItemEntity?> GetById(Guid id);
    Task<List<ItemEntity>?> GetOwned(Guid id);
}