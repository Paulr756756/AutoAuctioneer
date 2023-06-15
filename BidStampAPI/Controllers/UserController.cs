using API_BidStamp.Models.UserRequestModels;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace API_BidStamp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        public UserController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User Already exists");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken(),
                RegistrationDate = DateTime.Now,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok("User successfully created!");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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
            else if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("User credentials not correct");
            }

            return Ok($"Welcome Back \nUser:{user.UserName}\nEmail:{user.Email}");
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
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

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
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
            if(!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt)){
                return BadRequest("Passwords do not match");
            }


            List<Stamp> stamps = await _dbContext.Stamps.Where(s=>s.UserId==user.UserId).ToListAsync();
            List<Listing> listings = await _dbContext.Listings.Where(l => l.UserId == user.UserId).ToListAsync();

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok("User deleted successfully with id"+ user.UserId);
        }
    }
}
    
