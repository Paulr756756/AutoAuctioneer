using API_BidStamp.Models.UserRequestModels;
using API_BidStamp.Services.UserService;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_BidStamp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public UserController(DatabaseContext dbContext, IConfiguration config, IUserService userService)
        {
            _dbContext = dbContext;
            _config = config;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User Already exists");
            }

            /*CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);*/

            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password),
                /*PasswordSalt = passwordSalt,*/
                VerificationToken = CreateRandomToken(),
                RegistrationDate = DateTime.Now,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok("User successfully created!");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            else if (user.VerifiedAt == null)
            {
                return BadRequest("User not verified");
            }
            else if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash))
            {
                return BadRequest("User credentials not correct");
            }

            var token = CreateJwtToken(user);

            string response = $"Welcome Back \nUser:{user.UserName}\nEmail:{user.Email}\n" +
                $"your token is :{token}";

            return Ok(response);
        }


        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u=> u.VerificationToken==token);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            user.VerifiedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return Ok("User verified successfully");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            await _dbContext.SaveChangesAsync();
            return Ok("You may now reset your password");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.UtcNow)
            {
                return BadRequest("Invalid Token");
            }

            /*CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);*/

            user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);
            /*user.PasswordSalt = passwordSalt;*/
            user.ResetTokenExpires = null;
            user.PasswordResetToken = null;

            await _dbContext.SaveChangesAsync();
            return Ok("SuccessfullyResetted your password");
        }

        //TODO() : User Delete
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)){
                return BadRequest("Passwords do not match");
            }


            List<Stamp> stamps = await _dbContext.Stamps.Where(s=>s.UserId==user.UserId).ToListAsync();
            List<Listing> listings = await _dbContext.Listings.Where(l => l.UserId == user.UserId).ToListAsync();

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok("User deleted successfully with id"+ user.UserId);
        }


        private string CreateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Client")
            };

            /*var key = new SymmetricSecurityKey(Convert.FromBase64String(
                _config.GetSection("AppSettings:Token").Value!));*/
            
             var key = new SymmetricSecurityKey(Convert.FromBase64String(
                _config.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));
             

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet("getuserdetails"), Authorize]
        public ActionResult<object> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }
    }
}
    
