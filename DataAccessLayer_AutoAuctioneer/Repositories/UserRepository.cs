using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLibrary_AutoAuctioneer;
using DataAccessLibrary_AutoAuctioneer.Util;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer_AutoAuctioneer.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    bool CheckUserExists(string email);
    Task<Result<T>> StoreUser<T>(User user);
    Task<Result<T>> DeleteUser<T>(User user);
    Task<Result<User>> GetUserByEmail(string email);
    Task<Result<User>> GetUserByVToken(string token);
    Task<Result<User>> GetUserByPToken(string token);
    Task<Result<User>> GetUserById(Guid id);
    Task<Result<T>> ResetPassword<T>(User user);
    Task<Result<T>> ForgotPassword<T>(User user);
}

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext dbContext) : base(dbContext){ }

    public async Task<Result<T>> StoreUser<T>(User user)
    {
        return await StoreItemAsync<T>(user);
    }


    public bool CheckUserExists(string email)
    {
        if (_dbContext.Users.Any(u => u.Email == email)) return true;
        return false;
    }

    public async Task<Result<T>> DeleteUser<T>(User user)
    {
        /*var stamps = await _dbContext.Cars.Where(
                c => c.UserId == user.UserId)
            .ToListAsync();
        var listings = await _dbContext.Listings.Where(
                l => l.UserId == user.UserId)
            .ToListAsync();

        var carparts = await _dbContext.CarParts.Where(
            c => c.UserId == user.UserId
        ).ToListAsync();*/
        return await DeleteItemAsync<T>(user);
    }

    public async Task<Result<User>> GetUserByEmail(string email)
    {
        return await GetSingleItemAsync<User>(e => e.Email == email);
    }

    public async Task<Result<User>> GetUserByVToken(string token)
    {
        return await GetSingleItemAsync<User>(e => e.VerificationToken == token);
    }

    public async Task<Result<User>> GetUserByPToken(string token)
    {
        return await GetSingleItemAsync<User>(e => e.PasswordResetToken == token);
    }

    public async Task<Result<User>> GetUserById(Guid id)
    {
        return await GetSingleItemAsync<User>(e => e.UserId == id);
    }

    public async Task<Result<T>> ForgotPassword<T>(User user)
    {
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
        /*await _dbContext.SaveChangesAsync();*/
        return await UpdateItemAsync<T>(user);
    }

    public async Task<Result<T>> ResetPassword<T>(User user)
    {
        /*_dbContext.Update(user);
        await _dbContext.SaveChangesAsync();*/
        return await UpdateItemAsync<T>(user);
    }
}