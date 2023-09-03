using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.UserRequestModels;
public class UserUpdateRequest {

    [Required]
    public Guid UserId { get; set; }
    [Required]
    [MinLength(5)]
    [MaxLength(15)]
    public string? UserName { get; set; } = string.Empty;
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

/*    [DataType(DataType.Password)]
    [MinLength(6)]
    public string? Password { get; set; } = string.Empty;*/
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

