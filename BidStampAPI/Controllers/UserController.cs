using API_BidStamp.Models.UserRequestModels;
using API_BidStamp.Services.UserService;
using DataAccessLibrary_BidStamp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_BidStamp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public UserController(DatabaseContext dbContext, IConfiguration config,
        IUserService userService) {
        _config = config;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request) {
        if (await _userService.registerUser(request)) return Ok("User created");
        return BadRequest("Error");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request) {
        var token = await _userService.loginUser(request);

        /*string response = $"Welcome Back \nUser:{user.UserName}\nEmail:{user.Email}\n" +
            $"your token is :{token}";*/
        return Ok($"Welcome back: {token}");
    }


    [HttpPost("verify")]
    public async Task<IActionResult> Verify(string token) {
        if (_userService.verifyUser(token).Result) return Ok("User verified successfully");
        return BadRequest("User not verified");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email) {
        if (!_userService.forgotPassword(email).Result) return BadRequest("User not found");

        return Ok("You may now reset your password");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request) {
        if (!_userService.resetPassword(request).Result)
            return BadRequest("Couldn't reset your password");

        return Ok("SuccessfullyResetted your password");
    }

    [HttpDelete("delete-user")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteUser(DeleteUserRequest request) {
        if (_userService.deleteUser(request).Result) return Ok("User deleted successfully");

        return BadRequest("Couldn't delete user!");
    }

    [HttpGet("getuserdetails")]
    [Authorize(Roles = "Client")]
    public ActionResult<object> GetMe() {
        var userName = _userService.getMyName();
        return Ok(userName);
    }
}