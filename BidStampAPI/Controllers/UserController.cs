using API_AutoAuctioneer.Models.UserRequestModels;
using API_AutoAuctioneer.Services.UserService;
using DataAccessLibrary_AutoAuctioneer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public UserController(DatabaseContext dbContext, IConfiguration config,
        IUserService userService)
    {
        _config = config;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register<T>(UserRegisterRequest request)
    {
        if (await _userService.RegisterUser<T>(request)) return Ok("User created");
        return BadRequest("Error");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var token = await _userService.LoginUser(request);

        /*string response = $"Welcome Back \nUser:{user.UserName}\nEmail:{user.Email}\n" +
            $"your token is :{token}";*/
        return Ok($"Welcome back: {token}");
    }


    [HttpPost("verify")]
    public async Task<IActionResult> Verify(string token)
    {
        if (await _userService.VerifyUser(token)) return Ok("User verified successfully");
        return BadRequest("User not verified");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword<T>(string email)
    {
        if (! await _userService.ForgotPassword<T>(email)) return BadRequest("User not found");

        return Ok("You may now reset your password");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword<T>(ResetPasswordRequest request)
    {
        if (! await _userService.ResetPassword<T>(request))
            return BadRequest("Couldn't reset your password");

        return Ok("SuccessfullyResetted your password");
    }

    [HttpDelete("delete-user")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteUser<T>(DeleteUserRequest request)
    {
        if (await _userService.DeleteUser<T>(request)) return Ok("User deleted successfully");

        return BadRequest("Couldn't delete user!");
    }

    [HttpGet("getuserdetails")]
    [Authorize(Roles = "Client")]
    public ActionResult<object> GetMe()
    {
        var userName = _userService.GetMyName();
        return Ok(userName);
    }
}