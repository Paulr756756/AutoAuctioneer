using API_AutoAuctioneer.Models.UserRequestModels;

namespace API_AutoAuctioneer.Services.UserService;

public interface IUserService
{
    string GetMyName();
    Task<bool> RegisterUser<T>(UserRegisterRequest request);
    Task<bool> DeleteUser<T>(DeleteUserRequest request);
    Task<string> LoginUser(UserLoginRequest request);
    Task<bool> VerifyUser(string token);
    Task<bool> ForgotPassword<T>(string email);

    Task<bool> ResetPassword<T>(ResetPasswordRequest request);
    //private string createJwtToken(User user);
    //string createRandomToken();
}