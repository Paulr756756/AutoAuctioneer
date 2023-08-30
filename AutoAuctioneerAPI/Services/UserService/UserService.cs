using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API_AutoAuctioneer.Models.UserRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API_AutoAuctioneer.Services.UserService;

public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public UserService(IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository, IConfiguration config)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _config = config;
    }


    public async Task<bool> RegisterUser(UserRegisterRequest request)
    {
        if (_userRepository.GetUserByEmail(request.Email).Result.Data != null)
        {
            return false;
        }
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email.ToLower(),
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password),
            VerificationToken = CreateRandomToken(),
            RegistrationDate = DateTime.UtcNow
        };
        var response = await _userRepository.StoreUser(user);
        if (response.IsSuccess)
        {
            return true;
        }

        return false;
        /*try
        {
            if (_userRepository.CheckUserExists(request.Email).Result) return false;
    
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword
                    (request.Password),
                /*PasswordSalt = passwordSalt,#1#
                VerificationToken = CreateRandomToken(),
                RegistrationDate = DateTime.UtcNow
            };
    
            var response = await _userRepository.StoreUser<T>(user);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }*/
    }
    
    public async Task<bool> VerifyUser(string token)
    {
        var response = await _userRepository.GetUserByVToken(token);
        if (!response.IsSuccess)
        {
            Console.WriteLine("Couldn't get user by Verification token" + response.ErrorMessage);
            return false;
        }

        if (response.Data == null)
        {
            Console.WriteLine("No such user present");
            return false;
        }
        response.Data.VerifiedAt = DateTime.UtcNow;
        response = _userRepository.UpdateUser(response.Data).Result;
        if(!response.IsSuccess)
        {
            Console.WriteLine("Couldn't update user :" + response.ErrorMessage);
            return false;
        }

        return true;
    }


    public async Task<string> LoginUser(UserLoginRequest request)
    {
        var response = await _userRepository.GetUserByEmail(request.Email);
        var user = response.Data;
        if (user == null)
            return "User not found";
        if (user.VerifiedAt == null)
            return "User not verified";
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash))
            return "User credentials not correct";
        return CreateJwtToken(user);
    }


    
    public string GetMyName()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        return "Try logging in first";
    }

    public async Task<bool> DeleteUser(DeleteUserRequest request)
    {
        var user = _userRepository.GetUserByEmail(request.Email).Result.Data;
        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) return false;
        var response = await _userRepository.DeleteUser(user);

        if (response.IsSuccess)
        {
            return true;
        }
        
        Console.WriteLine(response.ErrorMessage);
        return false;
    }

    public async Task<bool> ForgotPassword(string email)
    {
        var response = await _userRepository.GetUserByEmail(email);
        var user = response.Data;
        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }

        var token = CreateRandomToken();

        var result = await _userRepository.SetPasswordResetToken(token, user.UserId);
        if (result.IsSuccess)
        {
            return true;
        }
        Console.WriteLine(result.ErrorMessage);
        return false;
    }

    public async Task<bool> ResetPassword(ResetPasswordRequest request)
    {
        var response = await _userRepository.GetUserByPToken(request.Token);
        if (!response.IsSuccess)
        {
            Console.WriteLine("Couldn't fetch password token :" + response.ErrorMessage);
            return false;
        }
        
        var user = response.Data;

        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }
        if (user.ResetTokenExpires < DateTime.UtcNow)
        {
            Console.WriteLine("User reset token has expired");
        }
        
        user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);
        user.ResetTokenExpires = null;
        user.PasswordResetToken = null;
        var result =await _userRepository.ResetPassword(user);
        if (result.IsSuccess)
        {
            return true;
        }
        
        Console.WriteLine("Couldn't reset password :" + result.ErrorMessage);
        return false;
    }

    private string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, "Client"),
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
        };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(
            _config.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}