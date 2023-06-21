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
        private readonly IUserService _user_service;
        public UserController(DatabaseContext dbContext, IConfiguration config,
            IUserService userService)
        {
            _dbContext = dbContext;
            _config = config;
            _user_service = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if( await _user_service.registerUser(request)) {
                return Ok("User created");
            }
            return BadRequest("Error");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request){
            string token = await _user_service.loginUser(request);

            /*string response = $"Welcome Back \nUser:{user.UserName}\nEmail:{user.Email}\n" +
                $"your token is :{token}";*/
            return Ok(token);
        }


        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
          if(_user_service.verifyUser(token).Result) {
                return Ok("User verified successfully");
          }
            return BadRequest("User not verified");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (_user_service.forgotPassword(email).Result)
            {
                return BadRequest("User not found");
            }
           
            return Ok("You may now reset your password");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if(_user_service.resetPassword(request).Result) {
                return BadRequest("Couldn't reset your password");
            }
            
            return Ok("SuccessfullyResetted your password");
        }

        //TODO() : User Delete
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
        {
            if (_user_service.deleteUser(request).Result) {
                return Ok("User deleted successfully");
            }

            return BadRequest("Couldn't delete user!");
        }

        [HttpGet("getuserdetails"), Authorize]
        public ActionResult<object> GetMe() {
            var userName = _user_service.getMyName();
            return Ok(userName);
        }        
    }
}
    
