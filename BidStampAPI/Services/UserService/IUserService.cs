﻿using API_BidStamp.Models.UserRequestModels;

namespace API_BidStamp.Services.UserService;

public interface IUserService {
    string getMyName();
    Task<bool> registerUser(UserRegisterRequest request);
    Task<bool> deleteUser(DeleteUserRequest request);
    Task<string> loginUser(UserLoginRequest request);
    Task<bool> verifyUser(string token);
    Task<bool> forgotPassword(string email);

    Task<bool> resetPassword(ResetPasswordRequest request);
    //private string createJwtToken(User user);
    //string createRandomToken();
}