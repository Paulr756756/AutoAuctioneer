using API_AutoAuctioneer.Models.UserRequestModels;
using API_AutoAuctioneer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [HttpGet("details")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetDetails([FromQuery] Guid id) {
        var user = await _userService.GetUserById(id);
        if (user == null) return BadRequest("No such user present");

        return Ok(new {
            username = user.UserName,
            firstname =user.FirstName,
            lastname =user.LastName,
            email = user.Email,
            dateofbirth = user.DateOfBirth,
            address = user.Address,
            phone = user.Phone,
            registrationdate = user.RegistrationDate
        });
    }

    [HttpPost("update"), Authorize(Roles = "Client")]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest request ) {
        if (await _userService.UpdateUserInfo(request)) return Ok("User Updated Successfully");
        return BadRequest("User Couldn't be updated");
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] UserVerifyRequest request) {
        if (await _userService.VerifyUser(request.token,request.email)) return Ok("User verified successfully");
        return BadRequest("User not verified");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request) {
        var token = await _userService.LoginUser(request);
        return Ok(token);
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody, EmailAddress] string email) {
        if (!await _userService.ForgotPassword(email)) return BadRequest("User not found");

        return Ok("You may now reset your password");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] UserPasswordResetRequest request) {
        if (!(await _userService.ResetPassword(request)))
            return BadRequest("Couldn't reset your password");

        return Ok("SuccessfullyResetted your password");
    }

    [HttpDelete("delete"), Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteUser([FromBody] UserDeleteRequest request) {
        if (await _userService.DeleteUser(request)) return Ok("User deleted successfully");
        return BadRequest("Couldn't delete user!");
    }
}