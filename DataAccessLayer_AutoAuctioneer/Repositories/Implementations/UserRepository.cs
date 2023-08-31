using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using DataAccessLayer_AutoAuctioneer.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Implementations;

public class UserRepository : BaseRepository, IUserRepository {
    private readonly IConfiguration _config;
    private readonly ILogger _logger;
    public UserRepository(IConfiguration config, ILogger logger) : base(config) {
        _config = config;
        _logger = logger;
    }

    public async Task<List<User>?> GetAllUsers() {
        var sql = "select id, username from \"users\"";
        var response = await LoadData<User, dynamic>(sql, new { });

        if (!response.IsSuccess) {
            //Do some logging
            _logger.LogCritical($"Response not a success : {response.ErrorMessage}");
        }
        return response.Data;
    }

    public async Task<User?> GetUserByEmail(string email) {
        var sql = "select" +
            "id," +
            "username," +
            "email," +
            "address," +
            "phoneno," +
            "firstname," +
            "lastname," +
            "dateofbirth," +
            " from \"users\" where email = @Email";
        var response = await LoadData<User, dynamic>(sql, new { Email = email });
        if (!response.IsSuccess) {
            _logger.LogCritical($"Response not a success : {response.ErrorMessage}");
        }
        var user = response.Data?.First();
        return user;
    }

    public async Task<User?> GetUserByVerificationToken(string token) {
        var sql = "select id, username from user where verificationtoken=@Token";
        var result = await LoadData<User, dynamic>(sql, new { Token = token });
        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success {result.ErrorMessage}");
        }

        var user = result.Data?.First();
        return user;
    }

    public async Task<User?> GetUserByPasswordToken(string token) {
        var sql = "select id, username from user where passwordresettoken = @Token";
        var result = await LoadData<User, dynamic>(sql, new { Token = token });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
        }
        var user = result.Data?.First();
        return user;
    }

    public async Task<User?> GetUserById(Guid id) {
        var sql = "select * from users where id = @Id";
        var result = await LoadData<User, dynamic>(sql, new { Id = id });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success: {result.ErrorMessage}");
        }
        var user = result.Data?.First();
        return user;
    }

    public async Task<bool> RegisterUser(User user) {
        var sql = "insert into \"users\" (id, username, email, verificationtoken, passwordhash, phoneno, " +
            "address, registeredat, firstname, lastname) values" + "(@Id, @UserName, @Email, @VerificationToken, @PasswordHash,"
            + "@Phone, @Address, @RegistrationDate, @FirstName, @LastName)";

        var response = await SaveData(sql, new {
            Id = user.UserId,
            user.Email,
            user.PasswordHash,
            user.Phone,
            user.UserName,
            Firstname = user.FirstName,
            user.LastName,
            user.Address,
            user.RegistrationDate,
            user.VerificationToken
        });

        if (!response.IsSuccess) {
            _logger.LogCritical($"Response not a success : {response.ErrorMessage}");
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUser(User user, Guid id) {
        var sql = "update \"users\" set username=@Username, email = @Email, firstname=@FirstName, " +
            "lastname=@LastName, address=@Address, phoneno=@Phone" +
            "where id=@Id";
        var result = await SaveData(sql,
            new {
                Id = id,
                Username = user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Address,
                user.Phone
            });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    public async Task<bool> SetPasswordResetToken(string token, Guid userId) {
        var sql = "update \"users\" set passwordresettoken=@Token, resettokenexpires=@ExpireDate where id=@UserId";
        var date = DateTime.UtcNow.AddDays(1);
        var result = await SaveData(sql, new { Token = token, ExpireDate = date, UserId = userId });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    public async Task<bool> UpdatePassword(string passwordHash, string id) {
        var sql = "update \"users\" set passwordhash=@PasswordHash where id=@Id"
        var result = await SaveData(sql, new { PasswordHash = passwordHash, Id = id });

        if (!result.IsSuccess) {
            _logger.LogCritical($"Response not a success : {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteUser(Guid id) {
        var sql = "delete from \"users\" where id=@Id";
        var result = await SaveData(sql, new { Id = id });

        if (!result.IsSuccess) {
            //Log
            return false;
        }
        return true;
    }
}