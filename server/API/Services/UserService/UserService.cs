using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.Models.RequestModels;
using DataAccessLayer_AutoAuctioneer.Models;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace API.Services.UserService;

public class UserService : IUserService
{
    private readonly IConfiguration _config;

    private readonly ILogger<UserService> _logger;

/*    private readonly IHttpContextAccessor _httpContextAccessor;*/
    private readonly IUserRepository _userRepository;

    public UserService(IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository, IConfiguration config, ILogger<UserService> logger)
    {
/*        _httpContextAccessor = httpContextAccessor;*/
        _userRepository = userRepository;
        _config = config;
        _logger = logger;
    }

    public async Task<bool> RegisterUser(RegisterUserRequestModel request)
    {
        if (await _userRepository.GetUserByEmail(request.Email) != null)
        {
            _logger.LogError("Email is already registered with another account");
            return false;
        }

        var user = new UserEntity
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
        var isRegistered = await _userRepository.RegisterUser(user);
        if (isRegistered)
        {
            _logger.LogInformation("New UserEntity is created.");
            var sender = new EmailUser
            {
                UserName = "Auto-Auctioneer",
                Email = "wolfl756756@gmail.com",
                Password = "iayjneoksmowcglg"
            };
            var recepient = new EmailUser
            {
                UserName = user.UserName,
                Email = user.Email.ToLower()
            };
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sender.UserName, sender.Email));
            message.To.Add(new MailboxAddress(recepient.UserName, recepient.Email));
            message.Subject = "Verify your Email account";
            var href = "\"https://localhost:44388/user/verify\"";
            message.Body = new TextPart("html")
            {
                Text = $@"
                <html>
                    <h1>Hey {recepient.UserName}!</h1><br/>
                    <h3>Verify Your email account</h3>
                    <p>
                        Copy the given token and paste it on the link given below
                        <br/>
                        <code>
                        {user.VerificationToken}
                        <code>
                        <a href={href}>Verify</a>
                    <p>
                </html>
                "
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(sender.Email, sender.Password);

                client.Send(message);
                _logger.LogInformation("Verification Email sent to id : {id}", recepient.Email);
                client.Disconnect(true);
            }

            return true;
        }

        return false;
    }

    public async Task<bool> UpdateUserInfo(UpdateUserRequest request)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null)
        {
            _logger.LogError("UserEntity does not exist with ID : {id}", request.UserId);
            return false;
        }

        if (request.UserName != null) user.UserName = request.UserName;
        if (request.Email != null) user.Email = request.Email;
        if (request.FirstName != null) user.FirstName = request.FirstName;
        if (request.LastName != null) user.LastName = request.LastName;
        if (request.Phone != null) user.Phone = request.Phone;
        if (request.Address != null) user.Address = request.Address;
        if (request.DateOfBirth != null) user.DateOfBirth = request.DateOfBirth;

        var response = await _userRepository.UpdateUser(user);
        if (response) return true;
        return false;
    }

    public async Task<bool> VerifyUser(string token, string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            _logger.LogInformation("No such user present");
            return false;
        }

        if (user.VerificationToken != token) _logger.LogError("Your token does not match");
        user.VerifiedAt = DateTime.UtcNow;
        var response = await _userRepository.VerifyUser(user);
        if (!response)
        {
            _logger.LogInformation("Unable to Update user");
            return false;
        }

        _logger.LogInformation($"UserEntity with id : {user.Id} updated");
        return true;
    }

    public async Task<string> LoginUser(LoginUserRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
        {
            _logger.LogInformation("UserEntity does not exist");
            return "UserEntity not found";
        }

        if (user.VerifiedAt == null)
        {
            _logger.LogInformation("UserEntity not verified");
            return "UserEntity not verified";
        }

        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash))
        {
            _logger.LogInformation("Incorrect UserEntity Creds");
            return "UserEntity not found";
        }

        return CreateJwtToken(user);
    }

    public async Task<UserEntity?> GetUserById(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            _logger.LogError("No such user present");
            return null;
        }

        return user;
    }

    public async Task<bool> ForgotPassword(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            Console.WriteLine("UserEntity does not exist");
            return false;
        }

        var token = CreateRandomToken();

        var result = await _userRepository.SetPasswordResetToken(token, (Guid)user.Id);
        if (result)
        {
            var sender = new EmailUser
            {
                UserName = "Auto-Auctioneer",
                Email = "wolfl756756@gmail.com",
                Password = "iayjneoksmowcglg"
            };
            var recepient = new EmailUser
            {
                UserName = user.UserName!,
                Email = user.Email!.ToLower()
            };
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sender.UserName, sender.Email));
            message.To.Add(new MailboxAddress(recepient.UserName, recepient.Email));
            message.Subject = "Reset your account Password";
            var href = "\"https://localhost:44388/user/reset\"";
            message.Body = new TextPart("html")
            {
                Text = $@"
                <html>
                    <h1>Hey {recepient.UserName}!</h1><br/>
                    <h3>Verify Change your account password</h3>
                    <p>
                        Copy the given token and paste it on the link given below to reset your account
                        <br/>
                        <code>
                        {user.VerificationToken}
                        <code>
                        <a href={href}>Verify</a>
                    <p>
                </html>
                "
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(sender.Email, sender.Password);

                client.Send(message);
                _logger.LogInformation("Verification Email sent to id : {id}", recepient.Email);
                client.Disconnect(true);
            }

            return true;
        }

        return false;
    }

    public async Task<bool> ResetPassword(ResetPasswordRequest request)
    {
        var user = await _userRepository.GetUserByPasswordToken(request.Token);

        if (user == null)
        {
            Console.WriteLine("UserEntity does not exist");
            return false;
        }

        if (user.ResetTokenExpires < DateTime.UtcNow) Console.WriteLine("UserEntity reset token has expired");

        user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);
        var result = await _userRepository.UpdatePassword(user.PasswordHash, (Guid)user.Id!);
        if (result)
        {
            _logger.LogInformation("Updated password of user with id: {id}", user.Id);
            return true;
        }

        _logger.LogError("Couldn't reset password.");
        return false;
    }

    public async Task<bool> DeleteUser(DeleteUserRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
        {
            _logger.LogInformation("UserEntity Does not exist");
            return false;
        }

        if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) return false;
        var response = await _userRepository.DeleteUser((Guid)user.Id!);

        if (response) return true;
        _logger.LogError("Response false DeleteUser()");
        return false;
    }

    private string CreateJwtToken(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Role, "Client"),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()!)
        };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(
            _config.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(1),
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

public class EmailUser
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
}