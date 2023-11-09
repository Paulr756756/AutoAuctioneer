using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Client.Models.RequestModels;

public class VerifyUserRequest
{
    [Required, EmailAddress]
    public string EmailAddress { get; set; }
    
    [Required]
    public string VerificationToken { get; set; }
}

public class ResetPasswordRequest {
    [Required, EmailAddress]
    public string EmailAddress { get; set; }
    
    [Required, PasswordPropertyText]
    public string Password { get; set; }
    
    [Required, PasswordPropertyText, Compare("Password")]
    public string ConfirmPassword { get; set; }
}