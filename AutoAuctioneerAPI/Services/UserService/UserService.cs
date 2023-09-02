using System.Security.Cryptography;
using API_AutoAuctioneer.Models.UserRequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;

namespace API_AutoAuctioneer.Services.UserService;

public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository, IConfiguration config, ILogger<UserService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _config = config;
        _logger = logger;
    }


    public async Task<bool> RegisterUser(UserRegisterRequest request)
    {
        if (await _userRepository.GetUserByEmail(request.Email) != null)
        {
            _logger.LogError($"Email is already registered with another account");
            return false;
        }
        
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email.ToLower(),
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password),
            VerificationToken = CreateRandomToken(),
            RegistrationDate = DateTime.UtcNow.Date,
            Phone = request.Phone,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address
        };
        var id = await _userRepository.RegisterUser(user);
        if (id)
        {
            _logger.LogInformation($"New User is created with id {id}");
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateUserInfo(UserUpdateRequest request) {
        var user = await _userRepository.GetUserById(request.UserId);
        if(user == null) {
            _logger.LogError("User does not exist with ID : {id}", request.UserId);
            return false;
        }
        if(request.UserName != null) user.UserName = request.UserName;
        if (request.Email != null) user.Email = request.Email;
        if (request.FirstName != null) user.FirstName = request.FirstName;
        if (request.LastName != null) user.LastName = request.LastName;
        if (request.Phone != null) user.Phone = request.Phone;
        if (request.Address != null) user.Address = request.Address;
        if (request.DateOfBirth != null) user.DateOfBirth = request.DateOfBirth;

        var response = await _userRepository.UpdateUser(user);
        if (response) {
            return true;
        }
        return false;
    }
    
/*    public async Task<bool> VerifyUser(string token)
    {
        var user = await _userRepository.GetUserByVerificationToken(token); 

        if (user == null)
        {
            _logger.LogInformation($"No such user present");
            return false;
        }
        user.VerifiedAt = DateTime.UtcNow;
        var response = await _userRepository.UpdateUser(user, user.UserId);
        if(!response)
        {
            _logger.LogInformation($"Unable to Update user ");
            return false;
        }

        return true;
    }*/


/*    public async Task<string> LoginUser(UserLoginRequest request)
    {
        var response = await _userRepository.GetUserByEmail(request.Email);
        var user = response;
        if (user == null) {
            _logger.LogInformation("User does not exist");
            return "User not found";
        }   
        if (user.VerifiedAt == null) {
            _logger.LogInformation("User not verified");
            return "User not verified";
        }
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) {
            _logger.LogInformation("Incorrect User Creds");
            return "User not found";
        }
        return CreateJwtToken(user);
    }
*/
/*    public string GetMyName()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        return "Try logging in first";
    }*/

/*    public async Task<bool> DeleteUser(DeleteUserRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
        {
            _logger.LogInformation("User Does not exist");
            return false;
        }
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) return false;
        var response = await _userRepository.DeleteUser(user.UserId);

        if (response)
        {
            return true;
        }
        _logger.LogError("Response false");
        return false;
    }*/

/*    public async Task<bool> ForgotPassword(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        
        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }

        var token = CreateRandomToken();

        var result = await _userRepository.SetPasswordResetToken(token, user.UserId);
        if (result)
        {
            return true;
        }

        return false;
    }*/

/*    public async Task<bool> ResetPassword(ResetPasswordRequest request)
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
    }*/

/*    private string CreateJwtToken(User user)
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
    }*/

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}