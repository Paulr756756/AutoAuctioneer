using System.Data;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class PartRepository : BaseRepository, IPartRepository
{
    private readonly ILogger<PartRepository> _logger;

    public PartRepository(IConfiguration config, ILogger<PartRepository> logger) : base(config)
    {
        _logger = logger;
    }

    public async Task<PartEntity?> GetPartById(Guid? guid)
    {
        var sql = "select * from parts where id = @Id;";
        var result = await LoadData<PartEntity, dynamic>(sql, new { Id = guid });
        _logger.LogInformation("Executed sql statement : {sql}", sql);

        if (!result.IsSuccess) _logger.LogError("Unable to fetch part : {e}", result.ErrorMessage);

        return result.Data!.FirstOrDefault();
    }

    public async Task<List<PartEntity>?> GetAllParts()
    {
        var sql = "select * from \"parts\";";
        var result = await LoadData<PartEntity, dynamic>(sql, new { });
        _logger.LogInformation("Executed sql statement: {sql}", sql);

        if (!result.IsSuccess) _logger.LogError("Unable to get all parts : {e}", result.ErrorMessage);

        return result.Data;
    }

    public async Task<List<PartEntity>?> GetPartsOfSingleUser(Guid id)
    {
        var sql = "select parts.* from parts inner join items  on parts.id = items.id where items.userid = @Id;";


        var result = await LoadData<PartEntity, dynamic>(sql, new { Id = id });
        _logger.LogInformation("Executed Stored procedure {sp}", sql);

        if (!result.IsSuccess) _logger.LogInformation("Couldn't fetch parts of user : {id}", id);
        return result.Data;
    }

    public async Task<bool> StorePart(PartEntity part)
    {
        var sql = "insert_part";
        var command = new NpgsqlCommand
        {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, DBNull.Value).Direction = ParameterDirection.Output;
        command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, part.UserId!);
        command.Parameters.AddWithValue("_type", NpgsqlDbType.Integer, 1);
        command.Parameters.AddWithValue("_name", NpgsqlDbType.Text, part.Name!);
        command.Parameters.AddWithValue("_description", NpgsqlDbType.Text,
            part.Description == null ? DBNull.Value : part.Description);
        command.Parameters.AddWithValue("_category", NpgsqlDbType.Text,
            part.Category == null ? DBNull.Value : part.Category);
        command.Parameters.AddWithValue("_marketprice", NpgsqlDbType.Bigint,
            part.MarketPrice == null ? DBNull.Value : part.MarketPrice);
        command.Parameters.AddWithValue("_parttype", NpgsqlDbType.Integer, part.PartType!);
        command.Parameters.AddWithValue("_manufacturer", NpgsqlDbType.Text,
            part.Manufacturer == null ? DBNull.Value : part.Manufacturer);
/*        var parameters = new DynamicParameters();
        parameters.Add("_id", part.Id, DbType.Guid, ParameterDirection.Output);
        parameters.Add("_userid", part.UserId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("_type", 1);
        parameters.Add("_name", part.Name);
        parameters.Add("_description", part.Description);
        parameters.Add("_category", part.Category);
        parameters.Add("_marketprice", part.MarketPrice);
        parameters.Add("_parttype", part.PartType);
        parameters.Add("_manufacturer", part.Manufacturer);
*/

        var result = await SaveData<PartEntity>(command);
        _logger.LogInformation("Executed stored procedure : {sp}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Unable to store part : {e}", result.ErrorMessage);
            return false;
        }

        return true;
    }

    public async Task<bool> UpdatePart(PartEntity part)
    {
        var sql = "update_part";
/*        var parameters = new DynamicParameters();
        parameters.Add("_id", part.Id);
        parameters.Add("_name", part.Name);
        parameters.Add("_description", part.Description);
        parameters.Add("_category", part.Category);
        parameters.Add("_marketprice", part.MarketPrice);
        parameters.Add("_parttype", part.PartType);
        parameters.Add("_manufacturer", part.Manufacturer);*/
        var command = new NpgsqlCommand
        {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, part.Id);
        command.Parameters.AddWithValue("_name", NpgsqlDbType.Text, part.Name!);
        command.Parameters.AddWithValue("_description", NpgsqlDbType.Text,
            part.Description == null ? DBNull.Value : part.Description);
        command.Parameters.AddWithValue("_category", NpgsqlDbType.Text,
            part.Category == null ? DBNull.Value : part.Category);
        command.Parameters.AddWithValue("_marketprice", NpgsqlDbType.Bigint,
            part.MarketPrice == null ? DBNull.Value : part.MarketPrice);
        command.Parameters.AddWithValue("_parttype", NpgsqlDbType.Integer, part.PartType);
        command.Parameters.AddWithValue("_manufacturer", NpgsqlDbType.Text,
            part.Manufacturer == null ? DBNull.Value : part.Manufacturer);

        var result = await SaveData<PartEntity>(command);
        _logger.LogInformation("Executed the stored procedure : {sp}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't update part : {e}", result.ErrorMessage);
            return false;
        }

        return true;
    }

    public async Task<bool> DeletePart(Guid id)
    {
        var sql = "delete_item";
        var command = new NpgsqlCommand
        {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

        var result = await SaveData<PartEntity>(command);
        _logger.LogInformation("Executed sql statement : {sql}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogInformation(result.ErrorMessage);
            return false;
        }

        return true;
    }
}