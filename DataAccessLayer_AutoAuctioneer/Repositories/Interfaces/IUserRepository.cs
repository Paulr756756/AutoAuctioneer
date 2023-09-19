using DataAccessLayer_AutoAuctioneer.Models;

namespace DataAccessLayer_AutoAuctioneer.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<List<User>?> GetAllUsers();
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
        Task<User?> GetUserByPasswordToken(string token);
        Task<User?> GetUserByVerificationToken(string token);
        Task<bool> RegisterUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> VerifyUser(User user);
        Task<bool> SetPasswordResetToken(string token, Guid userId);
        Task<bool> UpdatePassword(string passwordHash, Guid id);
        Task<bool> DeleteUser(Guid id);
    }
}