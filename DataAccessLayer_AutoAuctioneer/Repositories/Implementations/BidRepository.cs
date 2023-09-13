using Dapper;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using DataAccessLibrary_AutoAuctioneer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Numerics;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;
public class BidRepository : BaseRepository, IBidRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<BidRepository> _logger;

    public BidRepository(IConfiguration config, ILogger<BidRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<Bid>?> GetAllBids() {
        var sql = "select * from bids";
        var result = await LoadData<Bid, dynamic>(sql, new { });
        _logger.LogInformation("Executed sql statement: {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get all the bids: {e}", result.ErrorMessage);
            return null;
        }

        return result.Data;
    }

    public async Task<List<Bid>?> GetBidsPerListing(Guid guid) {
        var sql = "select * from bids where listingid = @Id";
        var result = await LoadData<Bid, dynamic>(sql, new {Id=guid});
        _logger.LogInformation("Executed sql statement : {sql}", sql);
        if (!result.IsSuccess) {
            _logger.LogError("Couldn't get bids per listing : {e}", result.ErrorMessage);
            return new List<Bid>();
        }
        return result.Data;
    }

    public async Task<Bid?> GetBidById(Guid id) {
        var sql = "select * from bids where id=@Id";
        var result = await LoadData<Bid, dynamic>(sql, new {Id=id});
        _logger.LogInformation("Executed Sql statement : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't fetch bid by id: {e}", result.ErrorMessage);
            return null;
        }
        return result.Data.FirstOrDefault();
    }

    public async Task<bool> PostBid(Bid bid) {
        var sql = "insert_bid";
        var parameters = new DynamicParameters();
        parameters.Add("_id", bid.Id, DbType.Guid, ParameterDirection.Output);
        parameters.Add("_userid", bid.UserId);
        parameters.Add("_bidamount", bid.BidAmount);
        parameters.Add("_bidtime", bid.BidTime);
        
        var result = await SaveData(sql, parameters, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed Stored Procedure :{sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't post bid : {e}", result.ErrorMessage);
            return false;
        }
        _logger.LogInformation("Bid created with id {id}", parameters.Get<Guid>("_id"));
        return true;
    }

    public async Task<bool> UpdateBidAmt(BigInteger amount, Guid id) {
        var sql = "update bids set bidamount=@Amount where id=@Id";
        var result = await SaveData(sql, new { Amount = amount, Id = id }, null);
        _logger.LogInformation("Sql statement executed : {sql}", sql);

        if (!result.IsSuccess) {
            _logger.LogError("Couldn't update the bid amount : {e}", result.ErrorMessage);
            return false;
        }

        return false;
    }

    public async Task<bool> DeleteBid(Guid id) {
        var sql = "delete_bid";
        var parameters = new DynamicParameters();
        parameters.Add("_id", id);

        var result = await SaveData(sql, parameters, cmdType:CommandType.StoredProcedure);
        _logger.LogInformation("Executed stored procedure : {sp}", sql);
        if (!result.IsSuccess) {
            _logger.LogError("Couldn't delete bid");
            return false;
        }
        _logger.LogInformation("Deleted bid with id : {id}", id);
        return true;
    }
}