using System.ComponentModel.DataAnnotations;

namespace Client.Models.RequestModels;

public class VerifyUserRequest
{
    [Required, EmailAddress]
    public string EmailAddress { get; set; }
    
    [Required]
    public string VerificationToken { get; set; }
}