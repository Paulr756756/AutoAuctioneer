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
        Task<Guid?> RegisterUser(User user);
/*        Task<bool> SetPasswordResetToken(string token, Guid userId);
        Task<bool> UpdatePassword(string passwordHash, string id);
        Task<bool> UpdateUser(User user, Guid id);
        Task<bool> DeleteUser(Guid id);*/
    }
}