using API_AutoAuctioneer.Models.UserRequestModels;

namespace API_AutoAuctioneer.Services.UserService;

public interface IUserService
{
/*    string GetMyName();*/
    Task<bool> RegisterUser(UserRegisterRequest request);
    Task<bool> UpdateUserInfo(UserUpdateRequest request);
/*    Task<bool> DeleteUser(DeleteUserRequest request);
    Task<string> LoginUser(UserLoginRequest request);
    Task<bool> VerifyUser(string token);
    Task<bool> ForgotPassword(string email);

    Task<bool> ResetPassword(ResetPasswordRequest request);*/
}