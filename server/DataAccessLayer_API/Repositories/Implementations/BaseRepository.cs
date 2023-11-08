using System.Data;
using DataAccessLayer_AutoAuctioneer.Util;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;


public class BaseRepository : IBaseRepository
{

    private readonly IConfiguration _config;
    public string connectionStringName { get; set; } = "aucdb";
    public BaseRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Result<T>> LoadData<T, U>(string sql, U parameters)
    {
        var connectionString = _config.GetConnectionString(connectionStringName);
        using (IDbConnection connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                var data = (await connection.QueryAsync<T>(sql, parameters)).ToList();
                return Result<T>.Success(data);
            }
            catch (Exception e)
            {
                return Result<T>.Failure(e.ToString());
            }
        }
    }
    
    public async Task<Result<T>> SaveData<T>(string sql, T parameters, CommandType? cmdType)
    {
        var connectionString = _config.GetConnectionString(connectionStringName);
        using (IDbConnection connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                await connection.ExecuteAsync(sql, parameters, null, null, cmdType);
                return Result<T>.SuccessNoData();
            }
            catch (Exception e)
            {
                return Result<T>.Failure(e.ToString());
            }
        }
    }

    public async Task<Result<T>> SaveData<T>(NpgsqlCommand command) {
        var connectionString = _config.GetConnectionString(connectionStringName);
        await using (var connection = new NpgsqlConnection(connectionString)) {
            command.Connection = connection;
            await command.Connection.OpenAsync();
            try {                
                await command.ExecuteNonQueryAsync();
                await command.Connection.CloseAsync();
                return Result<T>.SuccessNoData();
            }
            catch (Exception e) {
                await command.Connection.CloseAsync();
                return Result<T>.Failure(e.ToString());
            }
        }
    }
}