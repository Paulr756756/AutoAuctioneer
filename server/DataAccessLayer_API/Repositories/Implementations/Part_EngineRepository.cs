/*using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Implementations;
using Microsoft.Extensions.Configuration;

public class Part_EngineRepository : BaseRepository, IPart_EngineRepository {
    private readonly IConfiguration _config;
    public Part_EngineRepository(IConfiguration config) : base(config) {
        _config = config;
    }

    public async Task<Part_Engine?> GetEngineById(Guid guid) {
        var sql = "select * from (" +
            "select \"parts\".id as primaryid, name, description, category, marketprice," +
            "parttype, manufacturer, enginetype, displacement, horsepower, torque from \"parts\" inner join " +
            "\"parts_engine\" on \"parts\".id = \"parts_engine\".id) as \"engine\" where \"engine\".primaryId = @Id ;"; 
        var result = await LoadData<Part_Engine, dynamic>(sql, new { Id = guid });

        if (!result.IsSuccess) {
            //Log
        }
        return result.Data!.First();

    }

    *//*(TODO)
    public async Task<List<PartEntity>?> GetAllEngines() {
        var sql = "select * from \"parts\";";
        var result = await LoadData<PartEntity, dynamic>(sql, new { });

        if (!result.IsSuccess) {
            //Log
        }
        return result.Data;
    }

    public async Task<List<PartEntity>?> GetEnginesOfSingleUser(Guid guid) {
        var sql = "select * from \"parts\" where id = (select id from \"items\" where userid = @UserId)";
        var result = await LoadData<PartEntity, dynamic>(sql, new { UserId = guid });

        if (!result.IsSuccess) {
            //Log
        }
        return result.Data;
    }

    public async Task<bool> StoreEngine(PartEntity part) {
        var sql = "insert into \"parts\" (id, name, description, category, marketprice, parttype, manufacturer)"
            + "values (@Id, @Name, @Description, @Category, @MarketPrice, @PartType, @Manufacturer)";
        var result = await LoadData<PartEntity, dynamic>(sql, new {
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
    public async Task<bool> UpdateEngine(PartEntity part) {
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

    public async Task<bool> DeleteEngine(Guid id) {
        var sql = "delete from \"parts\" where id=@Id ";
        var result = await SaveData<dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            //Log
            return false;
        }
        return true;
    }
}
*/