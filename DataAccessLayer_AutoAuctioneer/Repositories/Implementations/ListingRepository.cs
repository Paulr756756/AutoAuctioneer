using Dapper;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class ListingRepository : BaseRepository, IListingRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<ListingRepository> _logger;

    public ListingRepository(IConfiguration config, ILogger<ListingRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<ListingEntity>?> GetAllListings() {
        var sql = "select * from listings";
        var result = await LoadData<ListingEntity, dynamic>(sql, new { });
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get all the listings. {e}", result.ErrorMessage);
            return null;
        }
        return result.Data;
    }


    public async Task<List<ListingEntity>?> GetOwnedListings(Guid guid) {
        var sql = "select * from listings where userid = @UserId";
        var result = await LoadData<ListingEntity, dynamic>(sql, new {UserId = guid});
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get all the listings : {e}", result.ErrorMessage);
            return null;
        }
        return result.Data;
    }

    public async Task<ListingEntity?> GetListingById(Guid id) {
        var sql = "select * from listings where id = @Id;";
        var result = await LoadData<ListingEntity, dynamic>(sql, new {Id= id});
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get the listing by ID : {e}", result.ErrorMessage);
            return null;
        }
        return result.Data!.FirstOrDefault();
    }

    public async Task<bool> PostListing(ListingEntity listing) {
        var sql = "insert_listing";
        var command = new NpgsqlCommand() {
            CommandType = CommandType.StoredProcedure,
            CommandText = sql,
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, DBNull.Value).Direction=ParameterDirection.Output;
        command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, listing.UserId);
        command.Parameters.AddWithValue("_itemid", NpgsqlDbType.Uuid, listing.ItemId);

        var result = await SaveData<ListingEntity>(command);
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't post listing : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteListing(Guid id) {
        var sql = "delete_listing";
        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure,
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);
        var result = await SaveData<ListingEntity>(command);
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);
        
        if(!result.IsSuccess) {
            _logger.LogError("Couldn't delete listing : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
}