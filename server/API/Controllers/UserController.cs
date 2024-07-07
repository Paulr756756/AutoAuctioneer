using System.ComponentModel.DataAnnotations;
using API.Models.RequestModels;
using API.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestModel request)
    {
        if (await _userService.RegisterUser(request)) return Ok(true);
        return BadRequest(false);
    }

    [HttpGet("details")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetDetails([FromQuery] Guid id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null) return BadRequest("No such user present");

        return Ok(new
        {
            username = user.UserName,
            firstname = user.FirstName,
            lastname = user.LastName,
            email = user.Email,
            dateofbirth = user.DateOfBirth,
            address = user.Address,
            phone = user.Phone,
            registrationdate = user.RegistrationDate
        });
    }

    [HttpPost("update")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        if (await _userService.UpdateUserInfo(request)) return Ok("UserEntity Updated Successfully");
        return BadRequest("UserEntity Couldn't be updated");
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] VerifyUserRequest request)
    {
        if (await _userService.VerifyUser(request.Token, request.Email)) return Ok("UserEntity verified successfully");
        return BadRequest("UserEntity not verified");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var token = await _userService.LoginUser(request);
        return Ok(token);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] [EmailAddress] string email)
    {
        if (!await _userService.ForgotPassword(email)) return BadRequest("UserEntity not found");

        return Ok("You may now reset your password");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!await _userService.ResetPassword(request))
            return BadRequest("Couldn't reset your password");

        return Ok("SuccessfullyResetted your password");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
    {
        if (await _userService.DeleteUser(request)) return Ok("UserEntity deleted successfully");
        return BadRequest("Couldn't delete user!");
    }
}