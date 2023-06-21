using API_BidStamp.Models.UserRequestModels;
using Azure.Core;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Repository_BidStamp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_BidStamp.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
      /*  private readonly DatabaseContext _db_context;*/
        private readonly IUserRepository _user_repository;
        private readonly IConfiguration _config;

        public UserService(IHttpContextAccessor httpContextAccessor,
          IUserRepository user_repository, IConfiguration config){
            _httpContextAccessor = httpContextAccessor;
            _user_repository = user_repository;
            _config = config;
        }
        

        public async Task<bool> registerUser(UserRegisterRequest request)
        {
            try {
                if (_user_repository.checkUserExists(request.Email).Result) {
                    return false;
                }

                var user = new User {
                    UserId = Guid.NewGuid(),
                    UserName = request.UserName,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword
                      (request.Password),
                    /*PasswordSalt = passwordSalt,*/
                    VerificationToken = createRandomToken(),
                    RegistrationDate = DateTime.Now,
                };

                await _user_repository.storeUser(user);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public async Task<string> loginUser(UserLoginRequest request) {
            User user = await _user_repository.getUserByEmail(request.Email);
            if (user == null) {
                return "User not found";
            } else if (user.VerifiedAt == null) {
                return "User not verified";
            } else if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) {
                return "User credentials not correct";
            }
            return createJwtToken(user);
        }

        private string createJwtToken(User user) {
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


        public async Task<bool> verifyUser(string token) {
            return await _user_repository.getUserByVToken(token);
        }

        public string getMyName()
        {
            var result = string.Empty;
            if(_httpContextAccessor.HttpContext != null) { 
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return result; 
        }

        private string createRandomToken() {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<bool> deleteUser(DeleteUserRequest request) {
            var user = _user_repository.getUserByEmail(request.Email).Result;
            if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash)) {
                return false;
            }
            await _user_repository.deleteUser(user);

            return true;
        }
        public async Task<bool> resetPassword(ResetPasswordRequest request){

            var user = await _user_repository.getUserByPToken(request.Token);

            if (user == null || user.ResetTokenExpires < DateTime.UtcNow) {
                return false;
            }

            /*CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);*/

            user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);
            /*user.PasswordSalt = passwordSalt;*/
            user.ResetTokenExpires = null;
            user.PasswordResetToken = null;
            await _user_repository.resetPassword(user);

            return true;
        }
        public async Task<bool> forgotPassword(string email) {
            var user = await _user_repository.getUserByEmail(email);
            if(user == null) { 
                return false;
            }

            user.PasswordResetToken = createRandomToken();
            
            await _user_repository.forgotPassword(user);
            return true;
        }

    }
}
