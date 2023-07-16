using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository_BidStamp.Models;

public interface IUserRepository {
    Task<bool> checkUserExists(string email);
    Task storeUser(User user);
    Task deleteUser(User user);
    Task<User> getUserByEmail(string email);
    Task<bool> getUserByVToken(string token);
    Task<User> getUserByPToken(string token);
    Task<User> getUserById(Guid id);
    Task resetPassword(User user);
    Task forgotPassword(User user);
}

public class UserRepository : IUserRepository {
    private readonly DatabaseContext _db_context;

    public UserRepository(DatabaseContext db_context) {
        _db_context = db_context;
    }

    public async Task storeUser(User user) {
        _db_context.Add(user);
        await _db_context.SaveChangesAsync();
    }


    public async Task<bool> checkUserExists(string email) {
        if (_db_context.Users.Any(u => u.Email == email)) return true;
        return false;
    }

    public async Task deleteUser(User user) {
        var stamps = await _db_context.Stamps.Where(
                s => s.UserId == user.UserId)
            .ToListAsync();
        var listings = await _db_context.Listings.Where(
                l => l.UserId == user.UserId)
            .ToListAsync();

        _db_context.Users.Remove(user);
        await _db_context.SaveChangesAsync();
    }

    public async Task<User> getUserByEmail(string email) {
        var user = await _db_context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<bool> getUserByVToken(string token) {
        var user = await _db_context.Users.FirstOrDefaultAsync(
            u => u.VerificationToken == token);
        if (user != null) {
            user.VerifiedAt = DateTime.UtcNow;
            await _db_context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<User> getUserByPToken(string token) {
        return await _db_context.Users.FirstOrDefaultAsync(
            u => u.PasswordResetToken == token);
    }

    public async Task<User> getUserById(Guid id) {
        return await _db_context.Users.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task forgotPassword(User user) {
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
        await _db_context.SaveChangesAsync();
    }
    
    public async Task resetPassword(User user) {
        _db_context.Update(user);
        await _db_context.SaveChangesAsync();
    }
}