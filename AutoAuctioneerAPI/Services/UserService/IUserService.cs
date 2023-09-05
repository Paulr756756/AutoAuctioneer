using API_AutoAuctioneer.Models.UserRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API_AutoAuctioneer.Services.UserService;

public interface IUserService
{
/*    string GetMyName();*/
    Task<bool> RegisterUser(UserRegisterRequest request);
    Task<bool> UpdateUserInfo(UserUpdateRequest request);
    Task<bool> VerifyUser(string token, string email);
    Task<string> LoginUser(UserLoginRequest request);
    Task<bool> ForgotPassword(string email);
    Task<bool> ResetPassword(UserPasswordResetRequest request);
    Task<User?> GetUserById(Guid id);
    Task<bool> DeleteUser(UserDeleteRequest request);
}