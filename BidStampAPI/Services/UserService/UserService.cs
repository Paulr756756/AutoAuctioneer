using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API_AutoAuctioneer.Models.UserRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories;
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


    public async Task<bool> RegisterUser<T>(UserRegisterRequest request)
    {
        if (_userRepository.GetUserByEmail(request.Email).Result.Data != null)
        {
            return false;
        }
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password),
        };
        var response = await _userRepository.StoreUser<T>(user);
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
        return response.IsSuccess;
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

    public async Task<bool> DeleteUser<T>(DeleteUserRequest request)
    {
        var user = _userRepository.GetUserByEmail(request.Email).Result.Data;
        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) return false;
        var response = await _userRepository.DeleteUser<T>(user);

        if (response.IsSuccess)
        {
            return true;
        }
        
        Console.WriteLine(response.ErrorMessage);
        return false;
    }

    public async Task<bool> ForgotPassword<T>(string email)
    {
        var response = await _userRepository.GetUserByEmail(email);
        var user = response.Data;
        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }

        user.PasswordResetToken = CreateRandomToken();

        var result = await _userRepository.ForgotPassword<T>(user);
        if (result.IsSuccess)
        {
            return true;
        }
        Console.WriteLine(result.ErrorMessage);
        return false;
    }

    public async Task<bool> ResetPassword<T>(ResetPasswordRequest request)
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
        var result =await _userRepository.ResetPassword<T>(user);
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
            new(ClaimTypes.Role, "Client")
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