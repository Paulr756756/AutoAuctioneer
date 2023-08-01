using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.UserRequestModels;

public class DeleteUserRequest
{
    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;
}