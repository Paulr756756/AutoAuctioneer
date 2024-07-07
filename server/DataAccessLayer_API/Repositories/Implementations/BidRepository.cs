using System.Data;
using DataAccessLayer_API.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class BidRepository : BaseRepository, IBidRepository
{
    private readonly IConfiguration _config;
    private readonly ILogger<BidRepository> _logger;

    public BidRepository(IConfiguration config, ILogger<BidRepository> logger) : base(config)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<List<BidEntity>?> GetAllBids()
    {
        var sql = "select * from bids";
        var result = await LoadData<BidEntity, dynamic>(sql, new { });
        _logger.LogInformation("Executed sql statement: {sql}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't get all the bids: {e}", result.ErrorMessage);
            return null;
        }

        return result.Data;
    }

    public async Task<List<BidEntity>?> GetOwned(Guid id)
    {
        var sql = "select * from bids where userid=@Id";
        var result = await LoadData<BidEntity, dynamic>(sql, new { Id = id });
        _logger.LogInformation("Executed sql statement: {sql}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't get owned bids: {e}", result.ErrorMessage);
            return null;
        }

        return result.Data;
    }

    public async Task<List<BidEntity>?> GetBidsPerListing(Guid guid)
    {
        var sql = "select * from bids where listingid = @Id";
        var result = await LoadData<BidEntity, dynamic>(sql, new { Id = guid });
        _logger.LogInformation("Executed sql statement : {sql}", sql);
        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't get bids per listing : {e}", result.ErrorMessage);
            return new List<BidEntity>();
        }

        return result.Data;
    }

    public async Task<BidEntity?> GetBidById(Guid id)
    {
        var sql = "select * from bids where id=@Id";
        var result = await LoadData<BidEntity, dynamic>(sql, new { Id = id });
        _logger.LogInformation("Executed Sql statement : {sql}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't fetch entity by id: {e}", result.ErrorMessage);
            return null;
        }

        return result.Data!.FirstOrDefault();
    }

    public async Task<bool> PostBid(BidEntity entity)
    {
        var sql = "insert_bid";
        var command = new NpgsqlCommand
        {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, DBNull.Value).Direction = ParameterDirection.Output;
        command.Parameters.AddWithValue("_bidtime", NpgsqlDbType.Timestamp, entity.BidTime!).Direction =
            ParameterDirection.Output;
        command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, entity.UserId!);
        command.Parameters.AddWithValue("_listingid", NpgsqlDbType.Uuid, entity.ListingId!);
        command.Parameters.AddWithValue("_bidamount", NpgsqlDbType.Bigint, entity.BidAmount);
        /*
                var parameters = new DynamicParameters();
                parameters.Add("_id", entity.Id, DbType.Guid, ParameterDirection.Output);
                parameters.Add("_userid", entity.UserId, DbType.Guid);
                parameters.Add("_listingid", entity.ListingId, DbType.Guid);
                parameters.Add("_bidamount", entity.BidAmount);
                parameters.Add("_bidtime", entity.BidTime, DbType.DateTime, ParameterDirection.Output);*/

        /*var result = await SaveData(sql, parameters, cmdType:CommandType.StoredProcedure);*/
        var result = await SaveData<BidEntity>(command);
        _logger.LogInformation("Executed Stored Procedure :{sql}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't post entity : {e}", result.ErrorMessage);
            return false;
        }

        _logger.LogInformation("BidEntity created with id {id} \t at timestamp: {t}", command.Parameters["_id"],
            command.Parameters["_bidtime"]);
        return true;
    }

    public async Task<bool> UpdateBidAmt(BidEntity entity)
    {
        var sql = "update bids set bidamount=@Amount where id=@Id";
        var command = new NpgsqlCommand(sql);
        command.Parameters.AddWithValue("Id", NpgsqlDbType.Uuid, entity.Id);
        command.Parameters.AddWithValue("Amount", NpgsqlDbType.Bigint, entity.BidAmount);

        var result = await SaveData<BidEntity>(command);
        _logger.LogInformation("Sql statement executed : {sql}", sql);

        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't update the entity amount : {e}", result.ErrorMessage);
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteBid(BidEntity entity)
    {
        var sql = "delete_bid";
        var command = new NpgsqlCommand
        {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, entity.Id);
        var result = await SaveData<BidEntity>(command);
        _logger.LogInformation("Executed stored procedure : {sp}", sql);
        if (!result.IsSuccess)
        {
            _logger.LogError("Couldn't delete entity");
            return false;
        }

        _logger.LogInformation("Deleted entity with id : {id}", entity.Id);
        return true;
    }
}