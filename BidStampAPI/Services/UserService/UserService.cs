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
    private readonly IUserRepository _user_repository;

    public UserService(IHttpContextAccessor httpContextAccessor,
        IUserRepository user_repository, IConfiguration config)
    {
        _httpContextAccessor = httpContextAccessor;
        _user_repository = user_repository;
        _config = config;
    }


    public async Task<bool> registerUser(UserRegisterRequest request)
    {
        try
        {
            if (_user_repository.checkUserExists(request.Email).Result) return false;

            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword
                    (request.Password),
                /*PasswordSalt = passwordSalt,*/
                VerificationToken = createRandomToken(),
                RegistrationDate = DateTime.UtcNow
            };

            await _user_repository.storeUser(user);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<string> loginUser(UserLoginRequest request)
    {
        var user = await _user_repository.getUserByEmail(request.Email);
        if (user == null)
            return "User not found";
        if (user.VerifiedAt == null)
            return "User not verified";
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash))
            return "User credentials not correct";
        return createJwtToken(user);
    }


    public async Task<bool> verifyUser(string token)
    {
        return await _user_repository.getUserByVToken(token);
    }

    public string getMyName()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        return result;
    }

    public async Task<bool> deleteUser(DeleteUserRequest request)
    {
        var user = _user_repository.getUserByEmail(request.Email).Result;
        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) return false;
        await _user_repository.deleteUser(user);

        return true;
    }

    public async Task<bool> forgotPassword(string email)
    {
        var user = await _user_repository.getUserByEmail(email);
        if (user == null) return false;

        user.PasswordResetToken = createRandomToken();

        await _user_repository.forgotPassword(user);
        return true;
    }

    public async Task<bool> resetPassword(ResetPasswordRequest request)
    {
        var user = await _user_repository.getUserByPToken(request.Token);

        if (user == null || user.ResetTokenExpires < DateTime.UtcNow) return false;

        /*CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);*/

        user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);
        /*user.PasswordSalt = passwordSalt;*/
        user.ResetTokenExpires = null;
        user.PasswordResetToken = null;
        await _user_repository.resetPassword(user);

        return true;
    }

    private string createJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, "Client")
        };

        //1.
        /*var key = new SymmetricSecurityKey(Convert.FromBase64String(
            _config.GetSection("AppSettings:Token").Value!));*/


        var key = new SymmetricSecurityKey(Convert.FromBase64String(
            _config.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));

        /*4
         var key = new SymmetricSecurityKey(Convert
            .FromBase64String("+7C70wuX/udADESFi7Wgww=="));*/
        /*3
        var randomBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        var key = new SymmetricSecurityKey(randomBytes);*/

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private string createRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}