using Microsoft.Extensions.Configuration;
using DataAccessLayer_AutoAuctioneer.Models;
using Microsoft.Extensions.Logging;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using System.Data;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class ItemRepository : BaseRepository, IItemRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<ItemRepository> _logger;
    public ItemRepository(IConfiguration config, ILogger<ItemRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<Item>?> GetOwnedItems(Guid id) {
        var sql = "select * from \"items\" where  userid=@Id ;";
        var result = await LoadData<Item, dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            //Log
        }

        return result.Data;
    }
    public async Task<Item?> GetItemById(Guid id) {
        var sql = "select * from items where id = @Id";
        var result = await LoadData<Item, dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't fetch item by id : {e}", result.ErrorMessage);
            return null;
        }

        return result.Data.FirstOrDefault();
    }

    public async Task<bool> DeleteItem(Guid id) {
        var sql = "delete_item";
        var result = await SaveData(sql, new { Id = id }, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Stored Procedure Executed : {sql}", sql);
        if (!result.IsSuccess) {
            _logger.LogError("Couldn't delete the item. {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
}