using System.ComponentModel.DataAnnotations;

namespace API_AutoAuctioneer.Models.UserRequestModels;
public class UserVerifyRequest {
    [Required, EmailAddress]
    public string email { get; set; }
    [Required]
    public string token { get; set; }
}