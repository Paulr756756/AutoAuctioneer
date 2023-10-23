using Dapper;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Data.SqlTypes;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class UserRepository : BaseRepository, IUserRepository {
    private readonly IConfiguration _config;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(IConfiguration config, ILogger<UserRepository> logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<UserEntity>?> GetAllUsers() {
        var sql = "select id, username from \"users\"";
        var result = await LoadData<UserEntity, dynamic>(sql, new { });

        if (!result.IsSuccess) {
            //Do some logging
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
        }
        return result.Data;
    }


    public async Task<UserEntity?> GetUserByEmail(string email) {
       /* var sql = "select " +
            "id," +
            "username," +
            "email," +
            "address," +
            "phoneno," +
            "firstname," +
            "lastname," +
            "dateofbirth" +*/
       var sql = "select *" + 
            " from \"users\" where email = @Email";
        var result = await LoadData<UserEntity, dynamic>(sql, new { Email = email });
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
        }
        var user = result.Data?.FirstOrDefault();
        return user;
    }

    public async Task<UserEntity?> GetUserByVerificationToken(string token) {
        var sql = "select id, username from user where verificationtoken=@Token";
        var result = await LoadData<UserEntity, dynamic>(sql, new { Token = token });
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success {result.ErrorMessage}");
        }

        var user = result.Data?.FirstOrDefault();
        return user;
    }

    public async Task<UserEntity?> GetUserByPasswordToken(string token) {
        var sql = "select * from users where passwordresettoken = @Token";
        var result = await LoadData<UserEntity, dynamic>(sql, new { Token = token });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
        }
        var user = result.Data?.FirstOrDefault();
        return user;
    }

    public async Task<UserEntity?> GetUserById(Guid id) {
        var sql = "select * from users where id = @Id ;";
        var result = await LoadData<UserEntity, dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success: {result.ErrorMessage}");
        }
        var user = result.Data?.FirstOrDefault();
        return user;
    }

    public async Task<bool> RegisterUser(UserEntity user) {
        var sql = "insert_user";
        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure,
        };

        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, DBNull.Value).Direction = ParameterDirection.Output;
        command.Parameters.AddWithValue("_username", NpgsqlDbType.Varchar, user.UserName!);
        command.Parameters.AddWithValue("_email", NpgsqlDbType.Varchar, user.Email!);
        command.Parameters.AddWithValue("_verificationtoken", NpgsqlDbType.Text, user.VerificationToken!);
        command.Parameters.AddWithValue("_address", NpgsqlDbType.Text, user.Address==null?DBNull.Value:user.Address);
        command.Parameters.AddWithValue("_registrationdate", NpgsqlDbType.Timestamp, user.RegistrationDate!);
        command.Parameters.AddWithValue("_phoneno", NpgsqlDbType.Text, user.Phone == null ? DBNull.Value : user.Phone);
        command.Parameters.AddWithValue("_firstname", NpgsqlDbType.Varchar, user.FirstName==null?DBNull.Value : user.FirstName);
        command.Parameters.AddWithValue("_lastname", NpgsqlDbType.Varchar, user.LastName==null?DBNull.Value : user.LastName);
        command.Parameters.AddWithValue("_dateofbirth", NpgsqlDbType.Date, user.DateOfBirth == null ? DBNull.Value : user.DateOfBirth);
        command.Parameters.AddWithValue("_passwordhash", NpgsqlDbType.Text, user.PasswordHash!);

        /*        var parameters = new DynamicParameters();
                parameters.Add("_id", user.Id, DbType.Guid, ParameterDirection.Output);
                parameters.Add("_username", user.UserName);
                parameters.Add("_email", user.Email);
                parameters.Add("_verificationtoken", user.VerificationToken);
                parameters.Add("_address", user.Address);
                parameters.Add("_registrationdate", user.RegistrationDate, DbType.Date);
                parameters.Add("_phoneno", user.Phone);
                parameters.Add("_firstname", user.FirstName);
                parameters.Add("_lastname", user.LastName);
                parameters.Add("_dateofbirth", user.DateOfBirth, DbType.Date);
                parameters.Add("_passwordhash", user.PasswordHash);*/

        /*        var result = await SaveData(sql, parameters, cmdType : CommandType.StoredProcedure);*/
        var result = await SaveData<UserEntity>(command);
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }
        _logger.LogInformation("UserEntity created with ID : {userid}",command.Parameters["_id"]);
        return true;
    }

    public async Task<bool> UpdateUser(UserEntity user) {
        var sql = "update_user";
        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure,
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, user.Id!);
        command.Parameters.AddWithValue("_username", NpgsqlDbType.Varchar, user.UserName!);
        command.Parameters.AddWithValue("_email", NpgsqlDbType.Varchar, user.Email!);
        command.Parameters.AddWithValue("_address", NpgsqlDbType.Text, user.Address==null?DBNull.Value : user.Address);
        command.Parameters.AddWithValue("_phoneno", NpgsqlDbType.Text, user.Phone==null?DBNull.Value : user.Phone);
        command.Parameters.AddWithValue("_firstname", NpgsqlDbType.Varchar, user.FirstName==null?DBNull.Value : user.FirstName);
        command.Parameters.AddWithValue("_lastname", NpgsqlDbType.Varchar, user.LastName==null?DBNull.Value : user.LastName);
        command.Parameters.AddWithValue("_dateofbirth", NpgsqlDbType.Date, user.DateOfBirth==null?DBNull.Value:user.DateOfBirth);

        /*        var parameters = new DynamicParameters();
                parameters.Add("_id", user.Id, DbType.Guid, ParameterDirection.InputOutput);
                parameters.Add("_username", user.UserName);
                parameters.Add("_email", user.Email);
                parameters.Add("_address", user.Address);
                parameters.Add("_phoneno", user.Phone);
                parameters.Add("_firstname", user.FirstName);
                parameters.Add("_lastname", user.LastName);
                parameters.Add("_dateofbirth", user.DateOfBirth, DbType.Date);

                var result = await SaveData(sql,parameters, cmdType: CommandType.StoredProcedure);*/

        var result = await SaveData<UserEntity>(command);
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }

        _logger.LogInformation("UserEntity updated with id : {userid}", command.Parameters["_id"]);
        return true;
    }

    public async Task<bool> VerifyUser(UserEntity user) {
        var sql = "update users set verifiedat= @VerifiedAt";
        var command = new NpgsqlCommand(sql);
        command.Parameters.AddWithValue("VerifiedAt", NpgsqlDbType.Timestamp, user.VerifiedAt!);
        var result = await SaveData<UserEntity>(command);
        if(!result.IsSuccess) {
            _logger.LogCritical("Failure : {message}", result.ErrorMessage);
            return false;
        }
        return true;
    }

    public async Task<bool> SetPasswordResetToken(string token, Guid userId) {
        var sql = "update \"users\" set passwordresettoken=@Token, resettokenexpires=@ExpireDate where id=@UserId";
        var command = new NpgsqlCommand(sql);
        var date = DateTime.UtcNow.AddDays(1);
        command.Parameters.AddWithValue("Token", token);
        command.Parameters.AddWithValue("ExpireDate", NpgsqlDbType.Timestamp, date);
        command.Parameters.AddWithValue("UserId", NpgsqlDbType.Uuid, userId);

        /*        var result = await SaveData(sql, new { Token = token, ExpireDate = date, UserId = userId }, null);*/
        var result = await SaveData<UserEntity>(command);
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    public async Task<bool> UpdatePassword(string passwordHash, Guid id) {
        var sql = "update \"users\" set passwordhash=@PasswordHash, resettokenexpires=null,passwordresettoken=null where id=@Id";
        var command = new NpgsqlCommand(sql);
        command.Parameters.AddWithValue("PasswordHash", passwordHash);
        command.Parameters.AddWithValue("Id", NpgsqlDbType.Uuid, id);
        /*var result = await SaveData(sql, new { PasswordHash = passwordHash, Id = id }, null);*/
        var result = await SaveData<UserEntity>(command);
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteUser(Guid id) {
        var sql = "delete_user";
        var command = new NpgsqlCommand() {
            CommandText = sql,
            CommandType = CommandType.StoredProcedure,
        };
        command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

        var result = await SaveData<UserEntity>(command);
        if (!result.IsSuccess) {
            _logger.LogError("DeleteUser() Couldn't delete user:{e}", result.ErrorMessage);
            return false;
        }
        return true;
    }
}