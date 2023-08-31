﻿using System.Data;
using System.Linq.Expressions;
using DataAccessLayer_AutoAuctioneer.Util;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;


public class BaseRepository : IBaseRepository
{

    private readonly IConfiguration _config;
    public string connectionStringName { get; set; } = "Default";
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

    public async Task<Result<T>> SaveData<T>(string sql, T parameters)
    {
        var connectionString = _config.GetConnectionString(connectionStringName);
        using (IDbConnection connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                await connection.ExecuteAsync(sql, parameters);
                return Result<T>.SuccessNoData();
            }
            catch (Exception e)
            {
                return Result<T>.Failure(e.ToString());
            }
        }
    }
}