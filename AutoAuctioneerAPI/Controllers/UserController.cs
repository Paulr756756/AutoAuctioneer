using API_AutoAuctioneer.Models.UserRequestModels;
using API_AutoAuctioneer.Services.UserService;
using DataAccessLibrary_AutoAuctioneer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController, Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public UserController(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]UserRegisterRequest request) {
        if (await _userService.RegisterUser(request)) return Ok("User created Successfully");
        return BadRequest("User couldn't be created");
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest request ) {
        if (await _userService.UpdateUserInfo(request)) return Ok("User Updated Successfully");
        return BadRequest("User Couldn't be updated");
    }

   /* [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
    {
        var token = await _userService.LoginUser(request);
        return Ok(token);
    }


    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody]string token)
    {
        if (await _userService.VerifyUser(token)) return Ok("User verified successfully");
        return BadRequest("User not verified");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody]string email)
    {
        if (! await _userService.ForgotPassword(email)) return BadRequest("User not found");

        return Ok("You may now reset your password");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
    {
        if (! await _userService.ResetPassword(request))
            return BadRequest("Couldn't reset your password");

        return Ok("SuccessfullyResetted your password");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteUser([FromBody]DeleteUserRequest request)
    {
        if (await _userService.DeleteUser(request)) return Ok("User deleted successfully");

        return BadRequest("Couldn't delete user!");
    }

    [HttpGet("getuserdetails")]
    [Authorize(Roles = "Client")]
    public IActionResult GetMe()
    {
        var userName = _userService.GetMyName();    
        return Ok(userName);
    }*/
}