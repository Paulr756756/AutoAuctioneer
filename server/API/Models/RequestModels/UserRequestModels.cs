using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels;

public class RegisterUserRequestModel
{
    [Required]
    [MinLength(5)]
    [MaxLength(15)]
    public string UserName { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateOfBirth { get; set; }
}

public class LoginUserRequest
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] [MinLength(6)] public string Password { get; set; }
}

public class DeleteUserRequest
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;
}

public class UpdateUserRequest
{
    [Required] public Guid UserId { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(15)]
    public string? UserName { get; set; } = string.Empty;

    [EmailAddress] [Required] public string Email { get; set; } = string.Empty;

    /*    [DataType(DataType.Password)]
        [MinLength(6)]
        public string? Password { get; set; } = string.Empty;*/
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

public class ResetPasswordRequest
{
    [Required] public string Token { get; set; } = string.Empty;
    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;
    [Required] [Compare("Password")] public string ConfirmPassword { get; set; } = string.Empty;
}

public class VerifyUserRequest
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Token { get; set; }
}