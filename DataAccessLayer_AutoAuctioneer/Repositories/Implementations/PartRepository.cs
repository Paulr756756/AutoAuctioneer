using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class PartRepository : BaseRepository, IPartRepository {
    public PartRepository(IConfiguration config) : base(config) { }

    public async Task<Part?> GetPartById(Guid guid) {
        var sql = "select * from \"parts\" where id = @Id;";
        var result = await LoadData<Part, dynamic>(sql, new { Id = guid });

        if (!result.IsSuccess) {
            //Log
        }
        return result.Data.First();

    }

    public async Task<List<Part>?> GetAlParts() {
        var sql = "select * from \"parts\";";
        var result = await LoadData<Part, dynamic>(sql, new { });

        if (!result.IsSuccess) {
            //Log
        }
        return result.Data;
    }

    public async Task<List<Part>?> GetPartsOfSingleUser(Guid guid) {
        var sql = "select * from \"parts\" where id = (select id from \"items\" where userid = @UserId)";
        var result = await LoadData<Part, dynamic>(sql, new { UserId = guid });

        if (!result.IsSuccess) {
            //Log
        }
        return result.Data;
    }

    public async Task<bool> StorePart(Part part) {
        var sql = "insert into \"parts\" (id, name, description, category, marketprice, parttype, manufacturer)"
            + "values (@Id, @Name, @Description, @Category, @MarketPrice, @PartType, @Manufacturer)";
        var result = await LoadData<Part, dynamic>(sql, new {
            part.Id,
            part.Name,
            part.Description,
            part.Category,
            part.MarketPrice,
            part.PartType,
            part.Manufacturer
        });

        if (!result.IsSuccess) {
            //Log
            return false;
        }

        return true;
    }
    public async Task<bool> UpdatePart(Part part) {
        var sql = "update \"parts\" set name=@Name, description=@Description, category=@Category" +
            "marketprice=@MarketPrice, parttype = @PartType, manufacturer = @Manufacturer";
        var result = await SaveData<dynamic>(sql, new {
            part.Name,
            part.Description,
            part.Category,
            part.MarketPrice,
            part.PartType,
            part.Manufacturer
        });

        if (!result.IsSuccess) {
            //Log
            return false;
        }
        return true;
    }

    public async Task<bool> DeletePart(Guid id) {
        var sql = "delete from \"parts\" where id=@Id ";
        var result = await SaveData<dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            //Log
            return false;
        }
        return true;
    }
}