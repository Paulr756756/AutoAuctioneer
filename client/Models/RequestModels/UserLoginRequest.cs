using System.ComponentModel.DataAnnotations;

namespace Client.Models;

public class UserLoginRequest
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Minimum Length is 6")]
    public string Password { get; set; }
}