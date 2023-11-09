using DataAccessLayer_API.Models;

namespace DataAccessLayer_AutoAuctioneer.Models;

public class UserEntity
{
    public Guid? Id { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? RegistrationDate { get; set; }
}