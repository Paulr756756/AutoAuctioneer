using Dapper;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class ListingRepository : BaseRepository, IListingRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<ListingRepository> _logger;

    public ListingRepository(IConfiguration config, ILogger<ListingRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<Listing>?> GetAllListings() {
        var sql = "select * from listings";
        var result = await LoadData<Listing, dynamic>(sql, new { });
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get all the listings. {e}", result.ErrorMessage);
            return null;
        }
        return result.Data;
    }


    public async Task<List<Listing>?> GetOwnedListings(Guid guid) {
        var sql = "select * from listings where userid = @UserId";
        var result = await LoadData<Listing, dynamic>(sql, new {UserId = guid});
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get all the listings : {e}", result.ErrorMessage);
            return null;
        }
        return result.Data;
    }

    public async Task<Listing?> GetListingById(Guid id) {
        var sql = "select * from listings where id = @Id;";
        var result = await LoadData<Listing, dynamic>(sql, new {Id= id});
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get the listing by ID : {e}", result.ErrorMessage);
            return null;
        }
        return result.Data!.FirstOrDefault();
    }

    public async Task<bool> PostListing(Listing listing) {
        var sql = "insert_listing";
        var parameters = new DynamicParameters();
        parameters.Add("_id", null, DbType.Guid, ParameterDirection.Output);
        parameters.Add("_userid", listing.UserId);
        parameters.Add("_itemid", listing.ItemId);

        var result = await SaveData(sql, parameters, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't post listing : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteListing(Guid id) {
        var sql = "delete_listing";
        var parameters = new DynamicParameters();
        parameters.Add("_id", id);
        var result = await SaveData(sql, parameters, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed Sql Statement : {sql}", sql);
        
        if(!result.IsSuccess) {
            _logger.LogError("Couldn't delete listing : {e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
}