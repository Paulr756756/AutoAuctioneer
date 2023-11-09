using API.Models.RequestModels;
using DataAccessLayer_AutoAuctioneer.Models;

namespace API.Services.UserService;

public interface IUserService
{
    Task<bool> RegisterUser(RegisterUserRequestModel request);
    Task<bool> UpdateUserInfo(UpdateUserRequest request);
    Task<bool> VerifyUser(string token, string email);
    Task<string> LoginUser(LoginUserRequest request);
    Task<bool> ForgotPassword(string email);
    Task<bool> ResetPassword(ResetPasswordRequest request);
    Task<UserEntity?> GetUserById(Guid id);
    Task<bool> DeleteUser(DeleteUserRequest request);
}