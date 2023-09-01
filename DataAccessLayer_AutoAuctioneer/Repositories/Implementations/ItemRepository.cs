/*using Microsoft.Extensions.Configuration;
using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class ItemRepository : BaseRepository {
    private readonly IConfiguration _config;
    public  ItemRepository(IConfiguration config) : base(config) {
    _config = config;
    }

    public async Task<List<Item>?> GetOwnedItems(Guid id) {
        var sql = "select * from \"items\" where  userid=@Id ;";
        var result = await LoadData<Item, dynamic>(sql, new { Id = id });

        if(!result.IsSuccess) {
            //Log
        }

        return result.Data;
    }

    public async Task<bool> AddItem(Item item) {
        var sql = "insert into \"items\" (id, userid, type) values (@Id, @UserId, @Type);";
        var result = await SaveData<dynamic>(sql, new { Id = item.Id, UserId = item.UserId, Type = item.Type });

        if(!result.IsSuccess) {
            //Log
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteItem(Guid id) {
        var sql = "delete * from \"items\" where id=@Id ;";
        var result = await SaveData<dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            //Log
            return false;
        }
        return true;
    }
}*/