using Dapper;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class PartRepository : BaseRepository, IPartRepository {
    private readonly ILogger<PartRepository> _logger;

    public PartRepository(IConfiguration config, ILogger<PartRepository> logger) : base(config) {
        _logger = logger;
    }

    public async Task<Part?> GetPartById(Guid guid) {
        var sql = "select * from \"parts\" where id = @Id;";
        var result = await LoadData<Part, dynamic>(sql, new { Id = guid });
        _logger.LogInformation("Executed sql statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to fetch part : {e}", result.ErrorMessage);
        }

        return result.Data!.FirstOrDefault();
    }

    public async Task<List<Part>?> GetAllParts() {
        var sql = "select * from \"parts\";";
        var result = await LoadData<Part, dynamic>(sql, new { });
        _logger.LogInformation("Executed sql statement: {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to get all parts : {e}", result.ErrorMessage);
        }

        return result.Data;
    }

    public async Task<List<Part>?> GetPartsOfSingleUser(Guid id) {
        var sql = "get_partsofsingleuser";
        var result = await LoadData<Part, dynamic>(sql, new { _id = id });
        _logger.LogInformation("Executed Stored procedure {sp}", sql);

        if (!result.IsSuccess) {
            _logger.LogInformation("Couldn't fetch parts of user : {id}", id);
        }
        return result.Data;
    }

    public async Task<bool> StorePart(Part part) {
        var sql = "insert_part";
        var parameters = new DynamicParameters();
        parameters.Add("_id", null, DbType.Guid, ParameterDirection.Output);
        parameters.Add("_name", part.Name);
        parameters.Add("_description", part.Description);
        parameters.Add("_category", part.Category);
        parameters.Add("_marketprice", part.MarketPrice);
        parameters.Add("_parttype", part.PartType);
        parameters.Add("_manufacturer", part.Manufacturer);


        var result = await SaveData(sql, parameters, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed stored procedure : {sp}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Unable to store part : {e}", result.ErrorMessage);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdatePart(Part part) {
        var sql = "update_part";
        var parameters = new DynamicParameters();
        parameters.Add("_id", part.Id);
        parameters.Add("_name", part.Name);
        parameters.Add("_description", part.Description);
        parameters.Add("_category", part.Category);
        parameters.Add("_marketprice", part.MarketPrice);
        parameters.Add("_parttype", part.PartType);
        parameters.Add("_manufacturer", part.Manufacturer);

        var result = await SaveData<dynamic>(sql, parameters, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed the stored procedure : {sp}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't update part : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }

    public async Task<bool> DeletePart(Guid id) {
        var sql = "delete_item";
        var result = await SaveData(sql, new { _id = id }, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed sql statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogInformation($"Couldn't delete part : {result.ErrorMessage}");
            return false;
        }
        return true;
    }
}