using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    /*bool CheckUserExists(string email);*/
    Task<Result<User>> StoreUser(User user);
    Task<Result<User>> UpdateUser(User user);
    Task<Result<User>> DeleteUser(User user);
    Task<Result<User>> GetUserByEmail(string email);
    Task<Result<User>> GetUserByVToken(string token);
    Task<Result<User>> GetUserByPToken(string token);
    Task<Result<User>> GetUserById(Guid id);
    Task<Result<User>> ResetPassword(User user);
    Task<Result<User>> ForgotPassword(User user);
}

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext dbContext) : base(dbContext){ }

    public async Task<Result<User>> StoreUser(User user)
    {
        return await StoreItemAsync(user);
    }

    public async Task<Result<User>> UpdateUser(User user)
    {
        return await UpdateItemAsync(user);
    }
    /*public bool CheckUserExists(string email)
    {
        if (_dbContext.Users.Any(u => u.Email == email)) return true;
        return false;
    }*/

    public async Task<Result<User>> DeleteUser(User user)
    {
        return await DeleteItemAsync(user);
    }

    public async Task<Result<User>> GetUserByEmail(string email)
    {
        return await GetSingleItemAsync(e => e.Email == email);
    }

    public async Task<Result<User>> GetUserByVToken(string token)
    {
        return await GetSingleItemAsync(e => e.VerificationToken == token);
    }

    public async Task<Result<User>> GetUserByPToken(string token)
    {
        return await GetSingleItemAsync(e => e.PasswordResetToken == token);
    }

    public async Task<Result<User>> GetUserById(Guid id)
    {
        return await GetSingleItemAsync(e => e.UserId == id);
    }

    public async Task<Result<User>> ForgotPassword(User user)
    {
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
        /*await _dbContext.SaveChangesAsync();*/
        var response = await UpdateItemAsync(user);
        return response;
    }

    public async Task<Result<User>> ResetPassword(User user)
    {
        /*_dbContext.Update(user);
        await _dbContext.SaveChangesAsync();*/
        return await UpdateItemAsync(user);
    }
}