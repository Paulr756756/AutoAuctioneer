using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace API.Services.ItemService;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;
    private readonly IUserRepository _userRepository;

    public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _itemRepository = itemRepository;
        _userRepository = userRepository;
    }

    public async Task<ItemEntity?> GetById(Guid id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item != null) return item;
        _logger.LogInformation("No such item exists in the database with id: {guid}", id);
        return null;
    }

    public async Task<List<ItemEntity>?> GetOwned(Guid id)
    {
        var items = await _itemRepository.GetOwnedItems(id);
        if (items != null) return items;
        _logger.LogInformation("UserEntity contains no owned items");
        return null;
    }
}