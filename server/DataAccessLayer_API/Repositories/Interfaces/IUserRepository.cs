using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository
{
    Task<List<UserEntity>?> GetAllUsers();
    Task<UserEntity?> GetUserByEmail(string email);
    Task<UserEntity?> GetUserById(Guid id);
    Task<UserEntity?> GetUserByPasswordToken(string token);
    Task<UserEntity?> GetUserByVerificationToken(string token);
    Task<bool> RegisterUser(UserEntity user);
    Task<bool> UpdateUser(UserEntity user);
    Task<bool> VerifyUser(UserEntity user);
    Task<bool> SetPasswordResetToken(string token, Guid userId);
    Task<bool> UpdatePassword(string passwordHash, Guid id);
    Task<bool> DeleteUser(Guid id);
}