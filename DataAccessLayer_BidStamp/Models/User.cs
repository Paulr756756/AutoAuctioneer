using DataAccessLibrary_BidStamp.Models;

namespace DataAccessLayer_BidStamp.Models; 

public class User {
    public Guid UserId { get; set; }
    public string? UserName { get; set; }

    public string PasswordHash { get; set; }

    /* public byte[] PasswordSalt { get; set; } = new byte[32];*/
    public string? VerificationToken { get; set; }
    public DateTime VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    public List<Bid>? Bids = new();
    public List<Listing>? Listings = new();
    /*public List<Stamp>? Stamps = new();*/
    public List<Car>? Cars = new();
    public List<CarPart>? CarParts = new();
}
