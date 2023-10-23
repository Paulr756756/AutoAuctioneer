using Microsoft.Extensions.Configuration;
using DataAccessLayer_AutoAuctioneer.Models;
using Microsoft.Extensions.Logging;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class ItemRepository : BaseRepository, IItemRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<ItemRepository> _logger;
    public ItemRepository(IConfiguration config, ILogger<ItemRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<ItemEntity>?> GetOwnedItems(Guid id) {
        var sql = "select * from \"items\" where  userid=@Id ;";
        var result = await LoadData<ItemEntity, dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            //Log
        }

        return result.Data;
    }
    public async Task<ItemEntity?> GetItemById(Guid id) {
        var sql = "select * from items where id=@Id";
        var result = await LoadData<ItemEntity, dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't fetch item by id : {e}", result.ErrorMessage);
            return null;
        }

        return result.Data!.FirstOrDefault();
    }

    public async Task<bool> DeleteItem(Guid id) {
        var sql = "delete_item";
        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);
        var result = await SaveData<ItemEntity>(command);

        _logger.LogInformation("Stored Procedure Executed : {sql}", sql);
        if (!result.IsSuccess) {
            _logger.LogError("Couldn't delete the item. {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
}